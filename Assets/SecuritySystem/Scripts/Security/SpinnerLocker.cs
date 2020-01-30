using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixsaoul.Security
{
    public enum SpinnerColor
    {
        Red = 0,
        Green = 1,
        Blue = 2
    }
    public class SpinnerLocker : LockInterface
    {
        [SerializeField] private List<Image> _holders;
        [SerializeField] private List<SpinnerColor> _currentColorValues;
        [SerializeField] private List<Button> _nexts;
        [SerializeField] private List<Button> _previous;

        [SerializeField] private Color _red;
        [SerializeField] private Color _green;
        [SerializeField] private Color _blue;

        [SerializeField] private List<SpinnerColor> _correctConfiguration;
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize(SecuritySystem securitySystem)
        {
            base.Initialize(securitySystem);
            for (int i = 0; i < _nexts.Count; i++)
            {
                int p = i;
                _nexts[i].onClick.AddListener(() => ToNextColor(p));
            }
            for (int i = 0; i < _previous.Count; i++)
            {
                int p = i;
                _previous[i].onClick.AddListener(() => ToPreviousColor(p));
            }
            for(int i = 0; i  <_holders.Count; i++)
            {
                SetColor(i, SpinnerColor.Red);
            }
        }

        /// <summary>
        /// To the color of the next.
        /// </summary>
        /// <param name="holderId">The holder identifier.</param>
        public void ToNextColor(int holderId)
        {
            SpinnerColor previous = _currentColorValues[holderId];
            SpinnerColor newsp = (SpinnerColor)((((int)previous + 1) + _currentColorValues.Count) % _currentColorValues.Count);
            SetColor(holderId, newsp);
        }
        public void ToPreviousColor(int holderId)
        {
            SpinnerColor previous = _currentColorValues[holderId];
            SpinnerColor newsp = (SpinnerColor)((((int)previous - 1)+_currentColorValues.Count) % _currentColorValues.Count);
            SetColor(holderId, newsp);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="color">The color.</param>
        private void SetColor(int id, SpinnerColor color)
        {
            _currentColorValues[id] = color;
            _holders[id].color = ToColor(_currentColorValues[id]);
        }

        private Color ToColor(SpinnerColor spc)
        {
            switch (spc)
            {
                case (SpinnerColor.Red):
                    return _red;
                    break;
                case (SpinnerColor.Green):
                    return _green;
                    break;
                case (SpinnerColor.Blue):
                    return _blue;
                    break;

            }
            return _red;
        }

        /// <summary>
        /// Computes the lock condition.
        /// </summary>
        public override void ComputeLockCondition()
        {
            base.ComputeLockCondition();
            bool isCorect = (_currentColorValues[0] == _correctConfiguration[0] && _currentColorValues[1] == _correctConfiguration[1] && _currentColorValues[2] == _correctConfiguration[2]);
            TryUnlock(isCorect);
        }
    }
}