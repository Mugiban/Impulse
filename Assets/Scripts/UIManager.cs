using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                }

                return instance;
            }
        }
    public UITweener gameCanvas;
    public UITweener gameOverCanvas;
    public UITweener fadeCanvas;
    
    private void OnEnable()
    {
        GameController.OnSceneLoaded += UpdateUI;
    }

    private void OnDisable()
    {
        GameController.OnSceneLoaded -= UpdateUI;
    }

    void UpdateUI(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            HideAll();
        }

        if (sceneName == "Game")
        {
            ShowGameInfo();
        }
    }
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void HideAll()
    {
        gameCanvas.Hide();
        gameOverCanvas.Hide();
    }

    public void ShowGameOver()
    {
        gameCanvas.Hide();
        gameOverCanvas.Show();
    }

    public void ShowGameInfo()
    {
        gameOverCanvas.Hide();
        gameCanvas.Show();
    }

    public void ResetForceAndDistance()
    {
        gameCanvas.GetComponentInChildren<DistanceIndicator>().ResetScale();
        gameCanvas.GetComponentInChildren<ForceIndicator>().ResetScale();
    }
}
