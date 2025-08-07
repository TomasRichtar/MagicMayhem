using System.Collections;
using UnityEngine;

namespace TastyCore.Extensions
{
   public static class TransformExtensions
   {
      #region Utils

      public static void DestroyChildren(this Transform transform)
      {
         foreach (Transform child in transform)
         {
            Object.Destroy(child.gameObject);
         }
      }

      #endregion

      #region Lerp
      
      public static IEnumerator LerpPosition(this Transform transform, Vector3 end, WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var start = transform.position;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, lerp);
            yield return eof;
         }

         transform.position = end;
      }

      public static IEnumerator LerpLocalPosition(this Transform transform, Vector3 end, WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var start = transform.localPosition;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(start, end, lerp);
            yield return eof;
         }

         transform.localPosition = end;
      }

      public static IEnumerator LerpRotation(this Transform transform, Quaternion end, WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var start = transform.rotation;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(start, end, lerp);
            yield return eof;
         }

         transform.rotation = end;
      }

      public static IEnumerator LerpLocalRotation(this Transform transform, Quaternion end, WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var start = transform.localRotation;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(start, end, lerp);
            yield return eof;
         }

         transform.localRotation = end;
      }

      public static IEnumerator LerpPositionAndRotation(this Transform transform, Vector3 endPos, Quaternion endRot,
         WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var startPos = transform.position;
         var startRot = transform.rotation;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, lerp);
            transform.rotation = Quaternion.Lerp(startRot, endRot, lerp);
            yield return eof;
         }

         transform.position = endPos;
         transform.rotation = endRot;
      }

      public static IEnumerator LerpLocalPositionAndRotation(this Transform transform, Vector3 endPos,
         Quaternion endRot, WaitForEndOfFrame eof,
         float duration = 1f)
      {
         var startPos = transform.localPosition;
         var startRot = transform.localRotation;
         var lerp = 0f;

         while (lerp <= 1)
         {
            lerp += duration * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, lerp);
            transform.localRotation = Quaternion.Lerp(startRot, endRot, lerp);
            yield return eof;
         }

         transform.localPosition = endPos;
         transform.localRotation = endRot;
      }
      
      #endregion
   }
}