using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ID.Audio
{
    
    [CustomEditor(typeof(Audio), true)]
    public class AudioEditor : Editor
    {
        [SerializeField] private ExtendedAudioSource previewer;

        public void OnEnable()
        {
            previewer = EditorUtility
                .CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(ExtendedAudioSource))
                .GetComponent<ExtendedAudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((Audio)target).Play(previewer);
            }
            
            if (GUILayout.Button("Stop"))
            {
                if (previewer.isPlaying)
                {
                    previewer.Stop();
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}