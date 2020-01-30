using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
namespace Pixsaoul.Security
{
    public enum Locker
    {
        Tree,
        Key,
        Spinner,
        Emergency,
        WiiU
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class SecuritySystem : MonoBehaviour
    {
        [SerializeField] private OverlayBlocker _blocker;
        [SerializeField] private AudioSource _blockerSound;

        [Header("Lock interfaces")]
        [SerializeField] private LockInterface _tree;
        [SerializeField] private LockInterface _key;
        [SerializeField] private LockInterface _spiner;
        [SerializeField] private LockInterface _emergency;
        [SerializeField] private LockInterface _wiiU;

        [Header("Selectors")]
        [SerializeField] private Button _treeSelector;
        [SerializeField] private Button _keySelector;
        [SerializeField] private Button _spinerSelector;
        [SerializeField] private Button _emergencySelector;
        [SerializeField] private Button _wiiUSelector;

        public void Initialize()
        {
            _tree.Initialize(this);
            _key.Initialize(this);
            _spiner.Initialize(this);
            _emergency.Initialize(this);
            _wiiU.Initialize(this);

            _treeSelector.onClick.AddListener(() => SelectInterface(Locker.Tree));
            _keySelector.onClick.AddListener(() => SelectInterface(Locker.Key));
            _spinerSelector.onClick.AddListener(() => SelectInterface(Locker.Spinner));
            _emergencySelector.onClick.AddListener(() => SelectInterface(Locker.Emergency));
            _wiiUSelector.onClick.AddListener(() => SelectInterface(Locker.WiiU));
        }

        /// <summary>
        /// Selects the interface.
        /// </summary>
        /// <param name="lockerType">Type of the locker.</param>
        public void SelectInterface(Locker lockerType)
        {
            switch (lockerType)
            {
                case (Locker.Emergency):
                    _emergency.SwitchEnabling();
                    _tree.Disable();
                    _key.Disable();
                    _spiner.Disable();
                    _wiiU.Disable();
                    break;
                case (Locker.Key):
                    _key.SwitchEnabling();
                    _tree.Disable();
                    _spiner.Disable();
                    _emergency.Disable();
                    _wiiU.Disable();
                    break;
                case (Locker.Spinner):
                    _spiner.SwitchEnabling();
                    _tree.Disable();
                    _key.Disable();
                    _emergency.Disable();
                    _wiiU.Disable();
                    break;
                case (Locker.Tree):
                    _tree.SwitchEnabling();
                    _key.Disable();
                    _spiner.Disable();
                    _emergency.Disable();
                    _wiiU.Disable();
                    break;
                case (Locker.WiiU):
                    _wiiU.SwitchEnabling();
                    _tree.Disable();
                    _key.Disable();
                    _spiner.Disable();
                    _emergency.Disable();
                    break;

            }
        }


        public void BlockSystem()
        {
            _blockerSound.Play();
            _blocker.Lock();
        }
    }
}