using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Chat
{
    public enum ConnectionStatus
    {
        Disconnected,
        Connected
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Photon.PunBehaviour" 
    public class NetworkService : Photon.PunBehaviour
    {
        [SerializeField] private Application _application;
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private ChatService _chatService;

        [SerializeField] private Color _connectedColor;
        [SerializeField] private Color _disconnectedColor;
        [SerializeField] private Image _connectionStatusIndicator;
        private ConnectionStatus _connectionStatus;
        [SerializeField] private float _pingDelay;

        private PhotonView _myPhotonView;

        public ConnectionStatus ConnectionStatus
        {
            get
            {
                return _connectionStatus;
            }

            set
            {
                _connectionStatus = value;
                switch(_connectionStatus)
                {
                    case (ConnectionStatus.Disconnected):
                        _connectionStatusIndicator.color = _disconnectedColor;
                        break;
                    case (ConnectionStatus.Connected):
                        _connectionStatusIndicator.color = _connectedColor;
                        break;
                }
            }
        }


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            ConnectionStatus = ConnectionStatus.Disconnected;
            PhotonPeer.RegisterType(typeof(Entry), new byte(), EntryToBytes, Entry.Deserialize);
            int rand = UnityEngine.Random.Range(0, int.MaxValue);
            PhotonNetwork.AuthValues = new AuthenticationValues(rand.ToString());
        }


        // Use this for initialization
        public void Start()
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("JoinRandom");
            PhotonNetwork.JoinRandomRoom();
            
        }

        public override void OnConnectedToMaster()
        {
            // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
            PhotonNetwork.JoinRandomRoom();
        }

        /// <summary>
        /// Called when [photon random join failed].
        /// </summary>
        public void OnPhotonRandomJoinFailed()
        {
            // we only use one room, hardcoded
            PhotonNetwork.CreateRoom("EscapeRoom");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room joined");
            StopCoroutine("PingLoop");
            StartCoroutine("PingLoop");
        }

        public override void OnDisconnectedFromPhoton()
        {
            base.OnDisconnectedFromPhoton();
            PhotonNetwork.ReconnectAndRejoin();
        }

        public override void OnConnectionFail(DisconnectCause cause)
        {
            base.OnConnectionFail(cause);
            PhotonNetwork.ReconnectAndRejoin();
        }

        public void Send(Entry entry)
        {
            _photonView.RPC("SendData", PhotonTargets.Others, entry);
        }        

        [PunRPC]
        public void SendData(Entry entry)
        {
            _chatService.Receive(entry);
        }

        private byte[] EntryToBytes(object entry)
        {
            return ((Entry)entry).Serialize();
        }

        #region background ping

        
        private IEnumerator PingLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(_pingDelay);
                _photonView.RPC("Ping", PhotonTargets.Others);
            }
        }

        [PunRPC]
        public void Ping()
        {
            //Debug.Log("Ping : " + Time.time);
            StopCoroutine("WaitForDisconnection");
            StartCoroutine("WaitForDisconnection");
        }

        private IEnumerator WaitForDisconnection()
        {
            ConnectionStatus = ConnectionStatus.Connected;
            yield return new WaitForSeconds(3 * _pingDelay);
            ConnectionStatus = ConnectionStatus.Disconnected;
        }
        

        #endregion
    }
}