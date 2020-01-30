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
    public class OverlayBlocker : MonoBehaviour
    {
        [SerializeField] private float _blockDuration;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private AnimationCurve _fadeCurve;

        [SerializeField] private Image _indicator;
        [SerializeField] private CanvasGroup _root;
        [SerializeField] private Text _timeLeft;

        private Coroutine _currentLock;
        private Coroutine _fadeCoroutine;

        /// <summary>
        /// Locks this instance.
        /// </summary>
        public void Lock()
        {
            if(_currentLock != null)
            {
                StopCoroutine(_currentLock);
            }
            _currentLock = StartCoroutine(LockC(_blockDuration));
        }

        private IEnumerator LockC(float duration)
        {
            StartCoroutine(ApplyLock(true));
            float startTime = Time.time;
            float completion = 0;
            do
            {
                completion = (Time.time - startTime) / duration;
                _indicator.fillAmount = 1-completion;
                _timeLeft.text = (duration*(1-completion)).ToString("0");
                yield return null;
            } while (completion < 1);
            StartCoroutine(ApplyLock(false));
        }


        /// <summary>
        /// Applies the lock.
        /// </summary>
        /// <param name="doLock">if set to <c>true</c> [do lock].</param>
        /// <returns></returns>
        private IEnumerator ApplyLock(bool doLock)
        {
            if(_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
            }
            _root.gameObject.SetActive(true);
            float completion = 0;
            float startTime = Time.time;
            do
            {
                float currentTime = Time.time;
                completion = (currentTime - startTime) / _fadeDuration;
                float value = doLock ? completion : 1f - completion;
                _root.alpha = _fadeCurve.Evaluate(value);
                yield return null;
            } while (completion < 1f);
            if (!doLock)
            {
                _root.gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// Forces the unlock.
        /// </summary>
        public void ForceUnlock()
        {
            StopAllCoroutines();
            _root.gameObject.SetActive(false);
        }
        /// <summary>
        /// Fades the overlay.
        /// </summary>
        /// <param name="fadeIn">if set to <c>true</c> [fade in].</param>
        /// <returns></returns>
        private IEnumerator FadeOverlay(bool fadeIn)
        {
            yield return null;
        }
    }
}