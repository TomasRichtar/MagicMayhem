using System;
using UnityEngine;

namespace TastyCore.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsMonoBehaviour(this Type t)
        {
            return typeof(MonoBehaviour).IsAssignableFrom(t);
        }
    }
}   