using Pixsaoul.Chat;
using Pixsaoul.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixsaoul
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct User
    {
        public int UserId;
        public string UserName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Application : MonoBehaviour
    {
        [SerializeField] private House _house;
        [SerializeField] private ChatService _chatService;
        [SerializeField] private SecuritySystem _securityService;
        [SerializeField] private GodMod _gameMasterInterface;

        [SerializeField] private User _player;
        [SerializeField] private User _gamemaster;
        [SerializeField] private User _engine;

        private User _currentUser;

#if (!UNITY_EDITOR && UNITY_ANDROID) || (USER && UNITY_EDITOR)
        public static bool IsMaster = false;
#else
        public static bool IsMaster = true;
#endif

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }

        public User Engine
        {
            get
            {
                return _engine;
            }
        }

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        void Awake()
        { //TODO TBU find a clean way to ahve a current User
            _currentUser = IsMaster ? _gamemaster : _player;
            _house.Initialize();
            _chatService.Initialize();

            _securityService.Initialize();
            _gameMasterInterface.Initialize(IsMaster);
        }


    }
}