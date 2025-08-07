using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedTimeTest : MonoBehaviour
{
    private Rigidbody _rigBody;
    private float _originalFixedDeltaTime;
    private float _localTimeScale = 1f;

    private Vector3 _originalVelocity;

    private void Start()
    {
        _rigBody = GetComponent<Rigidbody>();

        if (_rigBody == null)
        {
            Debug.LogError("RigidBody not found on this object.");
        }

        _originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_localTimeScale - 1f) > 0.01f)
        {
            Time.fixedDeltaTime = _originalFixedDeltaTime * _localTimeScale;
        }
        else
        {
            Time.fixedDeltaTime = _originalFixedDeltaTime;
        }
    }
    public void SetLocalTimeScale(float timeScale)
    {
        _localTimeScale = Mathf.Clamp(timeScale, 0.1f, 1f);

        if (_localTimeScale < 1f)
        {
            _originalVelocity = _rigBody.velocity;
            _rigBody.velocity *= _localTimeScale;
        }
        else
        {
            _rigBody.velocity = _originalVelocity;
        }
    }

    public void ResetTimeScale()
    {
        SetLocalTimeScale(1f);
    }

    private void OnDestroy()
    {
        Time.fixedDeltaTime = _originalFixedDeltaTime;
    }
}
