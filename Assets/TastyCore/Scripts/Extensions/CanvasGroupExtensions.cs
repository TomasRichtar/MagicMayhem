using System;
using System.Collections;
using UnityEngine;

namespace TastyCore.Extensions
{
    public static class CanvasGroupExtensions 
    {
        public static IEnumerator Show(this CanvasGroup canvasGroup, 
            WaitForEndOfFrame cachedYield, 
            float duration = 1f,
            Action callback = null)
        {
            var lerp = 0f;
            while (lerp <= 1)
            {
                lerp += Time.deltaTime / duration;
                canvasGroup.alpha = Mathf.Lerp(0, 1, lerp);
                yield return cachedYield;
            }

            canvasGroup.alpha = 1;
            callback?.Invoke();
        }

        public static IEnumerator Hide(this CanvasGroup canvasGroup, 
            WaitForEndOfFrame cachedYield,
            float duration = 1f,
            Action callback = null)
        {
            var lerp = 0f;
            while (lerp <= 1)
            {
                lerp += Time.deltaTime / duration;
                canvasGroup.alpha = Mathf.Lerp(1, 0, lerp);
                yield return cachedYield;
            }

            canvasGroup.alpha = 0;
            callback?.Invoke();
        }
    }
}