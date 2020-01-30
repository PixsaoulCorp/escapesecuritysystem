using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixsaoul.Chat
{
    /// <summary>
    /// Chat Service
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class ChatService : MonoBehaviour
    { //TODO TBU use an interface for Service.
        [SerializeField] private ChatView _chatView; // use callbacks
        [SerializeField] private NetworkService _networkService;
        [SerializeField] private Application _application;

        private Participant _localParticipant;
        private List<Entry> _historyModel;

        [SerializeField] private AudioSource _chatSound;

        //Use property only
        private static int _nextAvailableMessageID = 0;
        private static int NextAvailableMessageID
        {
            get
            {
                return _nextAvailableMessageID++;
            }
        }
        public void Initialize()
        {
            _historyModel = new List<Entry>();
            RegisterToLogs();
            _chatView.Initialize();
            _networkService.Initialize();
            User user = _application.CurrentUser;
            _localParticipant = new Participant(user.UserId, user.UserName); //TODO TBU
            //TODO TBU find away to have different participants on each devices
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string content)
        {
            Message message = new Message(NextAvailableMessageID, DateTime.Now, content);
            Entry newEntry = new Entry(_localParticipant, message, EntryType.Sent); //TODO TBU change it when you actually get is from server
            _networkService.Send(newEntry);
            // TODO interpret command for fun
            ManageNewEntry(newEntry);
        }

        /// <summary>
        /// Receives the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="player">The player.</param>
        public void Receive(Entry entry)
        {
            if (entry.Type == EntryType.Sent)
            {
                entry.Type = EntryType.Received;
                _chatSound.Play();
            } // we keep Engine messages as engine
            ManageNewEntry(entry);
        }

        /// <summary>
        /// Manages the new entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        private void ManageNewEntry(Entry entry)
        {
            //TODO TBU change message Received/Sent
            _historyModel.Add(entry);
            _chatView.AddEntry(entry);
        }

        #region Engine management

        private Participant _engineParticipant;       

        /// <summary>
        /// Registers to logs.
        /// </summary>
        private void RegisterToLogs()
        {
            if (!Application.IsMaster) {
                UnityEngine.Application.logMessageReceivedThreaded += OnLogReceived;
            }

            _engineParticipant = new Participant(_application.Engine.UserId, _application.Engine.UserName);
        }

        /// <summary>
        /// Called when [log received].
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="type">The type.</param>
        private void OnLogReceived(string log, string stackTrace, LogType type)
        {

            Entry newEntry = new Entry(_engineParticipant, new Message(NextAvailableMessageID, DateTime.Now, log), EntryType.Engine);
            switch (type)
            {
                case (LogType.Log):
                    ManageNewEntry(newEntry);
                    break;
                default:

                    break;
            }
            if (!Application.IsMaster)
            {
                _networkService.Send(newEntry);
            }
        }

#endregion

    }
}