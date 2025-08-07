using System.Collections;
using TastyCore.Patterns.ServiceLocator;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace TastyCore.Components.AudioManager
{
 public class AudioController : MonoRegistrable
    {
        public event Action BeforeAudio;
        public event Action AfterAudio;
        
        private readonly string MAIN = "MainAudio";
        private readonly string SECONDARY = "SecondaryAudio";
        private readonly string ONESHOT = "OneShotAudio";

        private AudioSource _mainAudio;
        private AudioSource _secondaryAudio;
        private AudioSource _oneShotAudio;

        private AudioLib _audioLib;

        private readonly float _duration = 3f;
        
        private WaitForEndOfFrame _frameEnd;
        private Coroutine _changeAudioRoutine;

        private void Awake()
        {
            _frameEnd = new WaitForEndOfFrame();
            
            var audioMixer = Resources.Load<AudioMixer>("Mixer");

            _mainAudio = CreateAudioSource(MAIN);
            _mainAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            _mainAudio.loop = true;

            _secondaryAudio = CreateAudioSource(SECONDARY);
            _secondaryAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            _secondaryAudio.loop = true;

            _oneShotAudio = CreateAudioSource(ONESHOT);
            _oneShotAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];

            _mainAudio.volume = 1;
            _secondaryAudio.volume = 0;
            

            AudioSource CreateAudioSource(string name)
            {
                var newObj = new GameObject(name);
                var newAudio = Instantiate(newObj, Vector3.zero, Quaternion.identity, gameObject.transform);
                newAudio.AddComponent<AudioSource>();
                Destroy(newObj);
                return newAudio.GetComponent<AudioSource>();
            }
            
            ServiceLocator.Register(this);
        }

        private void Start()
        {
            _audioLib = ServiceLocator.Get<AudioLib>();
        }

        public void ChangeAudio(AudioClip newAudio)
        {
            // Clip is the same
            if (_mainAudio.clip == newAudio) return;

            _secondaryAudio.clip = newAudio;
            if (_changeAudioRoutine != null)
                StopCoroutine(_changeAudioRoutine);

            (_mainAudio, _secondaryAudio) = (_secondaryAudio, _mainAudio);

            if (!_mainAudio.isPlaying)
                _mainAudio.Play();

            _changeAudioRoutine = StartCoroutine(Change());

            IEnumerator Change()
            {
                var currentMainAudio = _mainAudio.volume;
                var currentSecondaryAudio = _secondaryAudio.volume;

                var volumeValue = 0f;

                while (volumeValue < 1)
                {
                    volumeValue += Time.deltaTime / _duration;

                    _mainAudio.volume = Mathf.Lerp(currentMainAudio, 1, volumeValue);
                    _secondaryAudio.volume = Mathf.Lerp(currentSecondaryAudio, 0, volumeValue);

                    yield return _frameEnd;
                }

                _mainAudio.volume = 1;
                _secondaryAudio.volume = 0;
            }
        }

        public void PlayAudio(Audio vfx, bool invoke = false, AudioSource source = null)
        {
            var oneShot = _audioLib.GetAudio(vfx);

            if (oneShot == null) return;
            
            if(invoke)
            {
                BeforeAudio?.Invoke();
                StartCoroutine(EventInvoke(oneShot.length));
            }
            
            if (source == null)
            {
                _oneShotAudio.PlayOneShot(oneShot);
                return;
            }
            
            source.PlayOneShot(oneShot);
            
            IEnumerator EventInvoke(float delay)
            {
                yield return new WaitForSeconds(delay);
                AfterAudio?.Invoke();
            }
        }
    }
}