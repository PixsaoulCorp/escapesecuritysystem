using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Security
{
    public abstract class LockInterface : MonoBehaviour
    {
        [SerializeField] private float _onErrorLockDuration = 60f;
        [SerializeField] private GameObject _root;
        [SerializeField] private Button _tryUnlock;
        [SerializeField] private Room _linkedRoom;
        [SerializeField] private RoomStatus _targetRoomStatus;

        [SerializeField] private bool unlocked;
        [SerializeField] protected bool _enabled; // whether this interface is visible
        [SerializeField] protected bool _unlockable; // whether this interface can be unlocked

        [SerializeField] private Image _unlockedIcon;

        [SerializeField] private PhotonView _photonView;

        private SecuritySystem _securitySystem;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LockInterface"/> is unlocked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if unlocked; otherwise, <c>false</c>.
        /// </value>
        protected bool Unlocked
        {
            get
            {
                return unlocked;
            }

            set
            {
                unlocked = value;
                _unlockedIcon.gameObject.SetActive(value);
            }
        }

        public virtual void Initialize(SecuritySystem securityService)
        {
            _securitySystem = securityService;
            Unlocked = false;
            _tryUnlock.onClick.AddListener(ComputeLockCondition);
            _unlockable = true;
            Disable();
        }

        public void SwitchEnabling()
        {
            (_enabled ? (Action)Disable : Enable)();
        }
        /// <summary>
        /// Enables this instance.
        /// </summary>
        public virtual void Enable()
        {
            _enabled = true;
            _root.SetActive(true);
        }

        /// <summary>
        /// Disables this instance.
        /// </summary>
        public virtual void Disable()
        {
            _enabled = false;
            _root.SetActive(false);
        }

        public virtual void ComputeLockCondition()
        {

        }

        /// <summary>
        /// Tries the unlock. Should be called after any ComputeLockCondition
        /// </summary>
        /// <param name="isCorect">if set to <c>true</c> [is corect].</param>
        public void TryUnlock(bool isCorect)
        {
            if (!Unlocked) {
                if (isCorect)
                {
                    Unlock();
                    if (!Application.IsMaster)
                    {
                        _photonView.RPC("Unlock", PhotonTargets.Others);
                    }
                }
                else
                {
                    OnError();
                }
            }
        }

        [PunRPC]
        public void Unlock()
        {
            _linkedRoom.SetStatus(_targetRoomStatus);
            Unlocked = true;
        }

        public void OnError()
        {
            _securitySystem.BlockSystem();
        }

    }
}