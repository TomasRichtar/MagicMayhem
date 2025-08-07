using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace TastyCore.Utils
{
    public class FrameRateManager : MonoBehaviour
    {
        [Header("Frame Settings")]
        [SerializeField] private int _maxRate = 144;
        [SerializeField] private  float _targetFrameRate = 60;

        [Header("Debug")] 
        [SerializeField] private bool _showFps;
        [SerializeField] private float _frequencyUpdate = 1f;
        
        private float _currentFrameTime;

        private WaitForSecondsRealtime _waitForFrequency;
        private string _fps = "FPS: 0";
        GUIStyle _style = new GUIStyle();
        Rect _rect;
        
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _maxRate;
            _currentFrameTime = Time.realtimeSinceStartup;
            StartCoroutine(WaitForNextFrame());
        }

        private void Start()
        {
            if (!_showFps) return;
            
            _style.alignment = TextAnchor.UpperLeft;
            _style.fontSize = Screen.height * 3 / 100;
            _style.normal.textColor = new Color32(0, 200, 0, 255);
            _rect = new Rect(Screen.width * 90 / 100, 5, 0, Screen.height * 2 / 100);

            _waitForFrequency = new WaitForSecondsRealtime(_frequencyUpdate);
            StartCoroutine(ShowFps());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitForNextFrame()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _currentFrameTime += 1.0f / _targetFrameRate;
                var t = Time.realtimeSinceStartup;
                var sleepTime = _currentFrameTime - t - 0.01f;
                if (sleepTime > 0)
                    Thread.Sleep((int)(sleepTime * 1000));
                while (t < _currentFrameTime)
                    t = Time.realtimeSinceStartup;
            }
        }

        private IEnumerator ShowFps()
        {
            while (true)
            {
                var lastFrameCount = Time.frameCount;
                var lastTime = Time.realtimeSinceStartup;
                yield return _waitForFrequency;
            
                var frameCount = Time.frameCount - lastFrameCount;
                var timeSpan = Time.realtimeSinceStartup - lastTime;
            
                _fps = $"FPS: {Mathf.RoundToInt(frameCount / timeSpan)}";
            }
        }
        
        private void OnGUI()
        {
            if(_showFps)
                GUI.Label(_rect, _fps, _style);
        }
    }
}