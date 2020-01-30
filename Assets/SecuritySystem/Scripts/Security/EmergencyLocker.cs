using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Security
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class EmergencyLocker : LockInterface
    {
        [SerializeField] private string _correctValue;

        [Header("Numpad")]
        [SerializeField] private Button _one;
        [SerializeField] private Button _two;
        [SerializeField] private Button _three;
        [SerializeField] private Button _four;
        [SerializeField] private Button _five;
        [SerializeField] private Button _six;
        [SerializeField] private Button _seven;
        [SerializeField] private Button _eight;
        [SerializeField] private Button _nine;
        [SerializeField] private Button _del;
        [SerializeField] private Button _zero;
        [SerializeField] private Button _clear;

        private string _currentValue;
        [SerializeField] private Text _output;

        public string CurrentValue
        {
            get
            {
                return _currentValue;
            }

            set
            {
                _currentValue = value;
                _output.text = _currentValue;
            }
        }

        /// <summary>
        /// Initializes the specified security service.
        /// </summary>
        /// <param name="securityService">The security service.</param>
        public override void Initialize(SecuritySystem securityService)
        {
            _one.onClick.AddListener(() => AddDigit(1));
            _two.onClick.AddListener(() => AddDigit(2));
            _three.onClick.AddListener(() => AddDigit(3));
            _four.onClick.AddListener(() => AddDigit(4));
            _five.onClick.AddListener(() => AddDigit(5));
            _six.onClick.AddListener(() => AddDigit(6));
            _seven.onClick.AddListener(() => AddDigit(7));
            _eight.onClick.AddListener(() => AddDigit(8));
            _nine.onClick.AddListener(() => AddDigit(9));
            _del.onClick.AddListener(RemoveLast);
            _zero.onClick.AddListener(()=>AddDigit(0));
            _clear.onClick.AddListener(ClearCurrent);
            base.Initialize(securityService);
        }
        /// <summary>
        /// Computes the lock condition.
        /// </summary>
        public override void ComputeLockCondition()
        {
            base.ComputeLockCondition();
            bool isCorect = (_currentValue == _correctValue);
            TryUnlock(isCorect);
            ClearCurrent();
        }

        private void AddDigit(int digit)
        {
            CurrentValue += digit.ToString();
            _output.text = CurrentValue;
        }
        
        private void ClearCurrent()
        {
            CurrentValue = "";
        }

        private void RemoveLast()
        {
            if (CurrentValue.Length > 0)
            {
                CurrentValue = CurrentValue.Substring(0, CurrentValue.Length - 1);
            }
        }

    }
}