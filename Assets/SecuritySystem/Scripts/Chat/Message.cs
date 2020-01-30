using System;
using UnityEngine;

namespace Pixsaoul.Chat
{
    /// <summary>
    /// Message
    /// </summary
    [Serializable]
    public class Message
    {
        [SerializeField] private int _id;
        [SerializeField] private DateTime _creationTime;
        [SerializeField] private string _content;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="time">The time.</param>
        /// <param name="content">The content.</param>
        public Message(int id, DateTime time, string content)
        {
            _id = id;
            _creationTime = time;
            _content = content;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                return _creationTime;
            }            
        }

        public string Content
        {
            get
            {
                return _content;
            }
        }

        public override string ToString()
        {
            return $@"[{_creationTime.ToShortTimeString()}] {_content}";
        }
    }
}
