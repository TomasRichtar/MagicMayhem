using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TastyCore.Patterns.ServiceLocator;

namespace TastyCore.Components.AudioManager
{
    public class AudioLib : MonoRegistrable, ISerializationCallbackReceiver
    {
        Dictionary<string, AudioClip> _entityDictionary 
            = new Dictionary<string, AudioClip>();
        public Dictionary<string, AudioClip> Dictionary
        {
            get => _entityDictionary;
            set => _entityDictionary = value;
        }

        public AudioClip GetAudio(Audio vfx) => !Dictionary.ContainsKey(vfx.ToString()) || vfx == Audio.None
            ? null
            : Dictionary[vfx.ToString()];

        private void Awake()
        {
            ServiceLocator.Register(this);
        }

        public void Populate()
        {
            foreach (var a in Enum.GetValues(typeof(Audio)).Cast<Audio>())
            {
                if (a == Audio.None)
                {
                    _entityDictionary[a.ToString()] = null;
                    continue;
                }
                
                var audioClip = Resources.Load<AudioClip>($"Audio/{a.ToString()}");

                if (audioClip == null)
                {
                    Debug.LogError($"{a.ToString()} was not found in Resources/Audio");
                    continue;
                }

                _entityDictionary[a.ToString()] = audioClip;
            }
        }
        
        #region Custom Editor Helpers

        [Serializable]
        struct HolderPrefab
        {
            public Audio AudioEntity;
            public AudioClip Prefab;

            public HolderPrefab(string audioEntity, AudioClip holder)
            {
                AudioEntity = ToEnum(audioEntity);
                Prefab = holder;

                Audio ToEnum(string entity)
                {
                    return (Audio)Enum.Parse(typeof(Audio), entity, true);
                }
            }
        }

        [SerializeField] 
        [HideInInspector] 
        List<HolderPrefab> _holderList = new List<HolderPrefab>();

        public void OnBeforeSerialize()
        {
            _holderList.Clear();
            foreach (var hPrefab in _entityDictionary)
            {
                _holderList.Add(new HolderPrefab(hPrefab.Key, hPrefab.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            _entityDictionary = new Dictionary<string, AudioClip>();
            foreach (var entry in _holderList)
            {
                _entityDictionary.Add(entry.AudioEntity.ToString(), entry.Prefab);
            }
        }

        public AudioClip GetPrefab(string entity)
        {
            var result = _entityDictionary.TryGetValue(entity, out var prefab) ? prefab : null;
            return result;
        }

        #endregion
    }
}