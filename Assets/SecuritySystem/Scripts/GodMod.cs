using Pixsaoul.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Pixsaoul
{
    public class GodMod : MonoBehaviour
    {
        [SerializeField] private OverlayBlocker _blocker;
        [SerializeField] private GameObject _masterInterface;
        [SerializeField] private PhotonView _photonView;

        /// <summary>
        /// Initializes the specified is master.
        /// </summary>
        /// <param name="isMaster">if set to <c>true</c> [is master].</param>
        public void Initialize(bool isMaster)
        {
            _masterInterface.SetActive(isMaster);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (Application.IsMaster)
                {
                    ForceUnlockRPC();                    
                    _photonView.RPC("ForceUnlockRPC", PhotonTargets.Others);
                }
            }
        }

        [PunRPC]
        private void ForceUnlockRPC()
        {
            _blocker.ForceUnlock();
        }
    }
}
