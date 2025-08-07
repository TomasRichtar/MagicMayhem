using UnityEngine;
using UnityEngine.UI;

namespace TastyCore.Extensions
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 1);
            scrollRect.horizontalNormalizedPosition = 0;
        }
    
        public static void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}