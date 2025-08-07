using RichiGames;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewindableDestroy : RewindableObject
{
    public bool IsTemporary = true;
    private RewindableObjectPoolManager _rewindableObjectPoolManager;
    private void Awake()
    {
        _rewindableObjectPoolManager = FindFirstObjectByType<RewindableObjectPoolManager>();
    }

    public override void ContinueTime()
    {
    }

    public override void RecordStep()
    {
        if (_rewindData.Count > Mathf.Round(TimeController.Instance.RewindStorageLimit / Time.fixedDeltaTime))
        {
            _rewindData.RemoveLast();
        }
        RewindData rewindData = new RewindData(1);

        _rewindData.AddFirst(rewindData);
    }

    public override void RewindStep()
    {
        if (_rewindData.Count == 0)
        {
            if (IsTemporary)
            {
                _rewindableObjectPoolManager.RemoveObject(this);
                Destroy(gameObject);
            }
            return;
        }
        _rewindData.RemoveFirst();
    }

    public override void StopTime()
    {
    }
}
