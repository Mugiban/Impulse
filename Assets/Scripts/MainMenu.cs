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
    [SerializeField] private Button highscoreButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlay);
        creditsButton.onClick.AddListener(OnClickCredits);
        highscoreButton.onClick.AddListener(OnClickHighscore);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnClickCredits()
    {
        throw new NotImplementedException();
    }

    private void OnClickHighscore()
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
