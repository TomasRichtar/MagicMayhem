using System;
using TastyCore.Enums;
using UnityEngine;

namespace TastyCore.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 Flatten(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
        
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
        
        public static Vector3 ToVector3 (this Vector2 vector, Axis ignoreAxis)
        {
            // TODO 
            return new Vector3(vector.x, 0, vector.y);
        }
    }
}