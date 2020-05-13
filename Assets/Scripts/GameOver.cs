using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button exitButton;
    public Button playAgainButton;

    public TextMeshProUGUI highscoreText;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnClickPlayAgain);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickPlayAgain()
    {
        GameController.Instance.Reset();
    }

    private void OnClickExit()
    {
        GameController.Instance.LoadMainMenu();
    }
}
