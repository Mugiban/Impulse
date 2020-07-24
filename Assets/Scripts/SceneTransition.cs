using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }
    [SerializeField] private float fadeDuration;
    [SerializeField] private LeanTweenType easeType;

    public static event Action<int> OnFadeCompleted;
    public static bool isDone;
    private CanvasGroup canvasGroup;

    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void Start()
    {
        FadeOut();
    }

    public void FadeIn()
    {
        isDone = false;
        LeanTween.alphaCanvas(canvasGroup, 1, fadeDuration).setEase(easeType).setOnStart(UpdateFadingValue).setOnComplete(CancelFadingValue);
    }

    private void CancelFadingValue()
    {
        isDone  = true;
    }

    private void UpdateFadingValue()
    {
        isDone = false;
    }


    public void FadeOut()
    {
        isDone = false;
        LeanTween.alphaCanvas(canvasGroup, 0, fadeDuration).setEase(easeType).setOnStart(UpdateFadingValue).setOnComplete(CancelFadingValue);
    }

}