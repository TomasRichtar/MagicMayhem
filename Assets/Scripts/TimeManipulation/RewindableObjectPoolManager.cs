using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace RichiGames
{
    public class RewindableObjectPoolManager : RewindableObject
    {
        private Dictionary<RewindableDestroy, bool> _rewindableObject;

        protected override void Start()
        {
            base.Start();

            _rewindableObject = FindObjectsOfType<RewindableDestroy>()
                                .ToDictionary(obj => obj, obj => false);
        }

        public void AddObject(RewindableDestroy rewindableObject)
        {
            _rewindableObject.Add(rewindableObject, false);
        }

        public void DestroyObject(RewindableDestroy rewindableObject)
        {
            if (TimeController.Instance.IsRewinding) return;

            if (_rewindableObject.ContainsKey(rewindableObject))
            {
                _rewindableObject[rewindableObject] = true;
                rewindableObject.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Object not found in dictionary!");
            }
        }

        public void RemoveObject(RewindableDestroy rewindableObject)
        {
            _rewindableObject.Remove(rewindableObject);
        }

        public override void RecordStep()
        {
            if (_rewindData.Count > Mathf.Round(TimeController.Instance.RewindStorageLimit / Time.fixedDeltaTime))
            {
                _rewindData.RemoveLast();
            }
            Dictionary<RewindableDestroy, bool> snapshot = new Dictionary<RewindableDestroy, bool>(_rewindableObject);

            RewindData rewindData = new RewindData(snapshot);
           
            _rewindData.AddFirst(rewindData);
        }

        public override void RewindStep()
        {
            if (_rewindData.Count == 0)
            {
                return;
            }

            RewindData rewindData = _rewindData.First.Value;
            foreach (var item in rewindData.RewindableObjects)
            {
                item.Key.gameObject.SetActive(!item.Value);
            }

            _rewindData.RemoveFirst();
        }

        public override void StopTime()
        {
        }

        public override void ContinueTime()
        {
        }
    }
}
