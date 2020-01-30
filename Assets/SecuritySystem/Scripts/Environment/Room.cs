using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Security
{
    /// <summary>
    /// Room
    /// </summary>
    public class Room : MonoBehaviour
    {
        public Image _content;
        private House _parentRef;

        [SerializeField] private RoomStatus _initialStatus;

        [Header("Visualisation only")]
        [SerializeField] private RoomStatus _currentStatus;

        public void Initialize(House house)
        {
            _parentRef = house;
            SetStatus(_initialStatus, true);
        }

        /// <summary>
        /// Set Status
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(RoomStatus status, bool force = false)
        {
            if (force || status != _currentStatus )
            {
                Color c = _parentRef._unknownColor;
                switch (status)
                {
                    case (RoomStatus.Unknown):
                        c = _parentRef._unknownColor;
                        break;
                    case (RoomStatus.Locked):
                        c = _parentRef._lockedColor;
                        break;
                    case (RoomStatus.Unlocked):
                        c = _parentRef._unlockedColor;
                        break;
                    case (RoomStatus.Authorized):
                        c = _parentRef._availableColor;
                        break;
                }
                _content.color = c;
                _currentStatus = status;
                LogUnlocking();
            }
        }

        private void LogUnlocking()
        {
            if (_currentStatus == RoomStatus.Unlocked || _currentStatus == RoomStatus.Authorized)
            {
                Debug.Log($@"{this.name} is now {_currentStatus.ToString()}");
            }
        }
    }
}