using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreIndicator : MonoBehaviour
{

    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    
    private void OnEnable()
    {
        GameController.OnHighscoreUpdated += UpdateHighscore;
        UpdateHighscore();
    }

    private void OnDisable()
    {
        GameController.OnHighscoreUpdated -= UpdateHighscore;
    }

    void UpdateHighscore()
    {
        textMesh.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}


