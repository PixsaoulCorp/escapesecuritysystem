using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Security
{
    public class TreeLocker : LockInterface
    {
        [SerializeField] private Dropdown _input;

        [SerializeField] private int _correctValue;

        public override void Initialize(SecuritySystem securityService)
        {
            base.Initialize(securityService);
        }

        /// <summary>
        /// Computes the lock condition.
        /// </summary>
        public override void ComputeLockCondition()
        {
            base.ComputeLockCondition();
            bool isCorect = (_input.value == _correctValue);
            TryUnlock(isCorect);
            _input.value = 0;
        }
    }
}