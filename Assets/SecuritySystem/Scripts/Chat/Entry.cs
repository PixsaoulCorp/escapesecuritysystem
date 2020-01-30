using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Pixsaoul.Chat
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Entry
    {
        [SerializeField] private Participant _sender;
        [SerializeField] private Message _content;
        [SerializeField] private EntryType _type;

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public Message Content
        {
            get
            {
                return _content;
            }
        }

        /// <summary>
        /// Gets the sender identifier.
        /// </summary>
        /// <value>
        /// The sender identifier.
        /// </value>
        public Participant Sender
        {
            get
            {
                return _sender;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public EntryID ID
        {
            get { return new EntryID(_sender.Id, _content.Id); }
        }

        public EntryType Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class.
        /// </summary>
        /// <param name="senderID">The sender.</param>
        /// <param name="content">The content.</param>
        public Entry(Participant sender, Message content, EntryType type)
        {
            _sender = sender;
            _content = content;
            _type = type;
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            return Serializer.Serialize(this);
        }

        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static Entry Deserialize(byte[] data)
        {
            return (Entry) Serializer.Deserialize(data);
        }
    }
}
