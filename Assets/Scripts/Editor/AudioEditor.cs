using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Audio), true)]
public class AudioEditor : Editor
{
    [SerializeField] private AudioSource previewer;

    public void OnEnable()
    {
        previewer = EditorUtility
            .CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
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
            //((Audio)target).Play(previewer);
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