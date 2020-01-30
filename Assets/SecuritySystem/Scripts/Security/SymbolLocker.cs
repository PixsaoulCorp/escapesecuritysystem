using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pixsaoul.Tools;

namespace Pixsaoul.Security
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Pixsaoul.Security.LockInterface" />
    public class SymbolLocker : LockInterface
    {

        [SerializeField] private GameObject _keyboardRoot;
        [SerializeField] private KeyLetter _letterPrefab;
        [SerializeField] private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [SerializeField] private Text _currentField;
        [SerializeField] private string _correctValue = "LIE";

        [SerializeField] private Button _clearCurrentButton;

        private Dictionary<string, KeyLetter> _managedKeys;

       [SerializeField] private string _currentValue = "";

        public string CurrentValue
        {
            get
            {
                return _currentValue;
            }

            set
            {
                _currentValue = value;
                _currentField.text = value;
            }
        }

        /// <summary>
        /// Initializes the specified security service.
        /// </summary>
        /// <param name="securityService">The security service.</param>
        public override void Initialize(SecuritySystem securityService)
        {
            base.Initialize(securityService);
            ClearCurrent();
            InitializeKeyboard();
            _clearCurrentButton.onClick.AddListener(ClearCurrent);
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

        private void InitializeKeyboard()
        {
            _keyboardRoot.DeleteChildren();
            _managedKeys = new Dictionary<string, KeyLetter>();

            string consommedAlphabet = _alphabet;
            while(consommedAlphabet.Length > 0)
            {
                int index = Random.Range(0, consommedAlphabet.Length);
                CreateKey(consommedAlphabet[index].ToString());
                consommedAlphabet = consommedAlphabet.Remove(index, 1);
            }
        }

        /// <summary>
        /// Creates the key.
        /// </summary>
        /// <param name="key">The key.</param>
        private void CreateKey(string key)
        {
            KeyLetter newInstance = Instantiate<KeyLetter>(_letterPrefab);
            newInstance.transform.SetParent(_keyboardRoot.transform, false);
            newInstance.Initialize(key, AddChar);
        }

        /// <summary>
        /// Adds the character.
        /// </summary>
        /// <param name="c">The c.</param>
        private void AddChar(string c)
        {
            CurrentValue += c;
        }

        private void RemoveLast()
        {

        }

        private void ClearCurrent()
        {
            CurrentValue = string.Empty;
        }
    }
}