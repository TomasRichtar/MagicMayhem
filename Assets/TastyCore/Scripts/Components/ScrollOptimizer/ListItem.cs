using UnityEngine;

namespace TastyCore.Components.ScrollOptimizer
{
    public abstract class ListItem<T> : MonoBehaviour
    {
        public abstract void UpdateContent(int index, T context);
    }
}