using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixsaoul.Chat
{
    public struct EntryID
    {
        public int MessageID;
        public int SenderID;

        public EntryID(int messageId, int senderId)
        {
            MessageID = messageId;
            SenderID = senderId;
        }
    }
}