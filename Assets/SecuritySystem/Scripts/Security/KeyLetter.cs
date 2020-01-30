using System;
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
    public class KeyLetter : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _referenceChar;
        [SerializeField] private Text _visualChar;

        private Action<string> _callback;

        public string ReferenceChar
        {
            get
            {
                return _referenceChar;
            }

            set
            {
                _referenceChar = value;
                _visualChar.text = value;
            }
        }

        /// <summary>
        /// Initializes the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="callback">The callback.</param>
        public void Initialize(string c, Action<string> callback)
        {
            ReferenceChar = c;
            _callback = callback;
        }

        public void OnClick()
        {
            if (_callback != null)
            {
                _callback.Invoke(_referenceChar);
            }
        }
    }
}