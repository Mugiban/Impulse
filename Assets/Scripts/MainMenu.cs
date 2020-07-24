using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        creditsButton.onClick.AddListener(OnClickCredits);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickPlay()
    {
        GameController.Instance.LoadGame();
    }

    private void OnClickCredits()
    {
        throw new NotImplementedException();
    }

    private void OnClickExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
    }
}
