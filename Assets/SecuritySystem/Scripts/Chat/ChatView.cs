using Pixsaoul.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Chat
{ 

    public class ChatView : MonoBehaviour
    {
        [Header("Misc")]
        [SerializeField] private ChatService _chatService;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _sendButton;

        [Header("History content")]
        [SerializeField] private GameObject _root;
        [SerializeField] private EntryView _entrySentPrefab;
        [SerializeField] private EntryView _entryReceivedPrefab;
        [SerializeField] private EntryView _entryEnginePrefab;
        [SerializeField] private List<EntryView> _entries; // int are Entry ids

        public void Initialize()
        {
            _entries = new List<EntryView>();
            _sendButton.onClick.AddListener(TrySendMessage);
        }

        public void Clear()
        {
            _entries.Clear();
            _root.DeleteChildren();
        }

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void AddEntry(Entry entry)
        {
             EntryView newEntryView = Instantiate<EntryView>(GetPrefabFromType(entry.Type));
             newEntryView.transform.SetParent(_root.transform, false);
             newEntryView.Initialize(entry);
             _entries.Add(newEntryView);            
        }

        /// <summary>
        /// Gets the type of the prefab from.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private EntryView GetPrefabFromType(EntryType type)
        {
            switch (type)
            {
                case (EntryType.Engine):
                    return _entryEnginePrefab;
                    break;
                case (EntryType.Received):
                    return _entryReceivedPrefab;
                    break;
                case (EntryType.Sent):
                    return _entrySentPrefab;
                    break;
                default:
                    return _entryEnginePrefab;
            }
        }


        /// <summary>
        /// Tries the send message.
        /// </summary>
        public void TrySendMessage()
        {
            if (!string.IsNullOrEmpty(_inputField.text))
            {
                _chatService.Send(_inputField.text);
                _inputField.text = string.Empty; //Clear input field
            }
        }
    }
}