using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TastyCore.Components.AudioManager
{
    [CustomEditor(typeof(AudioLib))]
    public class AudioLibEditor : UnityEditor.Editor
    {
        List<Audio> _entities = new List<Audio>();
        bool _expanded = true;

        public override void OnInspectorGUI()
        {
            var behaviour = serializedObject.targetObject as AudioLib;
            if (behaviour == null) return;

            if(GUILayout.Button("Populate from resources"))
            {
                behaviour.Populate();
            }
        
            GUILayout.Space(10);
            DrawDefaultInspector();
        
            if (_entities.Count == 0) // Init
            {
                _entities = Enum.GetValues(typeof(Audio))
                    .Cast<Audio>()
                    .Where(a => a != Audio.None)
                    .ToList();

                var tempDictionary = new Dictionary<string, AudioClip>();
                foreach (var entity in _entities.Select(x => x.ToString()))
                {
                    tempDictionary.Add(entity, behaviour.GetPrefab(entity));
                }

                behaviour.Dictionary = tempDictionary;
            }

            //show prefab list
            _expanded = EditorGUILayout.Foldout(_expanded, "Audio Configuration");
            if (_expanded)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.BeginChangeCheck();

                    var tempDictionary = new Dictionary<string, AudioClip>();
                    foreach (var slot in _entities.Select(x => x.ToString()))
                    {
                        var prefab = (AudioClip)EditorGUILayout.ObjectField(slot, behaviour.Dictionary[slot],
                            typeof(AudioClip), false);
                        tempDictionary.Add(slot, prefab);
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Update Prefab");
                        behaviour.Dictionary = tempDictionary;
                        EditorUtility.SetDirty(target);
                    }
                }
            }
        }
    }
}