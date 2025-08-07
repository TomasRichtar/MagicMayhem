using System;
using System.Collections;
using TastyCore.Patterns.ServiceLocator;
using UnityEngine;

namespace TastyCore.Components.ScreenFader
{
    public class ScreenFader : MonoRegistrable
    {
        public event Action FadedOut;
        public event Action FadedIn;
        
        [Header("Configuration")]
        [SerializeField] private float _fadeOutDuration = 1f;
        [SerializeField] private float _fadeInDuration = 1f;
        [SerializeField] private bool _fadeOnStart;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            ServiceLocator.Register(this);
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            if (!_fadeOnStart) return;
            DoFadeIn();
        }

        public void FadeImmediate(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        #region FADE OUT
        // INTO BLACK COLOR
        
        public void DoFadeOut()
        {
            StartCoroutine(FadeOut(0));
        }
        
        /// <summary>
        /// Yieldeable Func - meant to be called from different class
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public IEnumerator FadeOut(float duration = 0)
        {
            var fadeDuration = duration == 0
                ? _fadeOutDuration
                : duration;
            
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / fadeDuration;
                yield return new WaitForEndOfFrame();
            }
            
            FadedOut?.Invoke();
            _canvasGroup.alpha = 1;
        }
        
        #endregion
        
        #region FADE IN 
        // OUT OF BLACK COLOR
        
        public void DoFadeIn()
        {
            StartCoroutine(FadeIn());
        }

        /// <summary>
        /// Yieldeable Func - meant to be called from different class
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public IEnumerator FadeIn(float duration = 0)
        {
            var fadeDuration = duration == 0
                ? _fadeInDuration
                : duration;

            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / fadeDuration;
                yield return new WaitForEndOfFrame();
            }

            FadedIn?.Invoke();
            _canvasGroup.alpha = 0;
        }
        
        #endregion
        
    }
}
