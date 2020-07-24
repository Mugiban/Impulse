using System;
using System.Collections;
using System.Collections.Generic;
using ID;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    public static event Action OnHighscoreUpdated;
    private static GameController instance;

    public static event Action<string> OnSceneLoaded;

    public int highscore;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
                {
                    instance = FindObjectOfType<GameController>();
                    if (instance == null)
                    {
                        instance = new GameObject("GameController").AddComponent<GameController>();
                    }
                }
            return instance;
        }
    }


    public void EndGame()
    {

        StartCoroutine(ReloadGame());
    }

    private void HandleHighScore()
    {
        int oldHighScore = PlayerPrefs.GetInt("highscore");
        if (oldHighScore < highscore)
        {
            PlayerPrefs.SetInt("highscore", highscore);
        }
        OnHighscoreUpdated?.Invoke();
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

    public void Reset()
    {
        
        Time.timeScale = 1f;
        PlayerController pController = FindObjectOfType<PlayerController>();
        pController.enabled = true;
        pController.Show();
        pController.ActivateRigidbody();
        ObstacleController.Instance.Reset();
        pController.Reset();
        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowGameInfo();
        uiManager.ResetForceAndDistance();

    }

    public void LoadMainMenu()
    {
        PlayerController pController = FindObjectOfType<PlayerController>();
        pController.Hide();
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    IEnumerator ReloadGame()
    {       
        PlayerController pController = FindObjectOfType<PlayerController>();
        highscore = pController.currentMeters;    
        HandleHighScore();
        Time.timeScale = 0.2f;


        UIManager uiManager = FindObjectOfType<UIManager>();
        uiManager.ShowGameOver();
        pController.HideLineRenderer();
        pController.Stop();
        pController.enabled = false;
        yield return new WaitForSeconds(.2f);
        
    }
    
    
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        Time.timeScale = 1;
        if (SceneTransition.Instance)
        {
            SceneTransition.Instance.FadeIn();
            while (!SceneTransition.isDone)
            {
                yield return null;
            }

        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        OnSceneLoaded?.Invoke(sceneName);

        if (SceneTransition.Instance)
        {
            SceneTransition.Instance.FadeOut();
        }
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsync("Game"));
    }
}