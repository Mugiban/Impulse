using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class UITweener : MonoBehaviour
{
    public enum UITransitionType
    {
        Move,
        Scale,
        Fade
    }

    [SerializeField] private UITransitionType transitionType = UITransitionType.Fade;
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutCubic;

    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float delay = 0f;

    [SerializeField] private bool loop;
    [SerializeField] private bool pingpong;
    [SerializeField] private bool startPositionOffset;    
    [SerializeField] private Vector3 from = Vector3.zero;
    [SerializeField] private Vector3 to = Vector3.one;

    [SerializeField] private GameObject objectToAnimate;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private LTDescr tweenObject;

    public bool startHide;
    public bool showOnEnable;
    public bool workOnDisable;
    
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnEnable()
    {
        if (startHide)
        {
            DisableAlphaCanvas();
        }
        if (showOnEnable)
        {
            Show();
        }
    }
    void EnableAlphaCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void DisableAlphaCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void Show()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }
        switch (transitionType)
        {
            case UITransitionType.Move:
                Move();
                break;
            case UITransitionType.Scale:
                Scale();
                break;
            case UITransitionType.Fade:
                Fade();
                break;
        }

        tweenObject.setDelay(delay);
        tweenObject.setEase(easeType);
        tweenObject.setIgnoreTimeScale(true);

        if (loop)
        {
            tweenObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            tweenObject.setLoopPingPong();
        }
    }
    
    public void Hide()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (objectToAnimate == null)
        {
            objectToAnimate = gameObject;
        }
        switch (transitionType)
        {
            case UITransitionType.Move:
                MoveOut();
                break;
            case UITransitionType.Scale:
                ScaleOut();
                break;
            case UITransitionType.Fade:
                FadeOut();
                break;
        }

        tweenObject.setDelay(delay);
        tweenObject.setEase(easeType);
        tweenObject.setIgnoreTimeScale(true);

        if (loop)
        {
            tweenObject.loopCount = int.MaxValue;
        }

        if (pingpong)
        {
            tweenObject.setLoopPingPong();
        }
    }
    private void Move()
    {
        EnableAlphaCanvas();
        if (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha = 1;
            rect.anchoredPosition = from;
        }

        if (startPositionOffset)
        {
            rect.anchoredPosition = from;
        }

        tweenObject = LeanTween.move(rect, to, duration);
    }
    private void Scale()
    {
        EnableAlphaCanvas();
        if (startPositionOffset)
        {
            rect.localScale = from;
        }

        tweenObject = LeanTween.scale(rect, to, duration);
    }
    private void Fade()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        if (startPositionOffset)
        {
            canvasGroup.alpha = from.x;
        }

        tweenObject = LeanTween.alphaCanvas(canvasGroup, to.x, duration);
    }
    
    private void MoveOut()
    {
        EnableAlphaCanvas();
        if (canvasGroup.alpha <= 1)
        {
            canvasGroup.alpha = 1;
        }

        if (startPositionOffset)
        {
            rect.anchoredPosition = to;
        }

        tweenObject = LeanTween.move(rect, from, duration);
    }
    private void ScaleOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if (startPositionOffset)
        {
            rect.localScale = to;
        }

        tweenObject = LeanTween.scale(rect, from, duration);
    }
    private void FadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        if (startPositionOffset)
        {
            canvasGroup.alpha = to.x;
        }

        tweenObject = LeanTween.alphaCanvas(canvasGroup, from.x, duration);
    }
    public void SetObjectToAnimate(GameObject newObject)
    {
        objectToAnimate = newObject;
    }

}