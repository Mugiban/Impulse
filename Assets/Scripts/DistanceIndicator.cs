using System;
using System.Collections;
using System.Collections.Generic;
using ID.Audio;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DistanceIndicator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private Audio obstacleSurpassedSound;
    [SerializeField] private Audio obstacleSurpassed10Sound;
    
    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerController.OnDistanceSurpassed += UpdateDistanceText;
        ResetScale();
        UpdateDistanceText(0);
    }

    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        PlayerController.OnDistanceSurpassed -= UpdateDistanceText;
    }

    void UpdateDistanceText(float distance)
    {
        UpdateScale(distance);
        textMesh.text = distance.ToString("0");
    }

    private void UpdateScale(float distance)
    {
        if (distance != 0)
        {
            
            if (distance % 10 == 0)
            {
                AudioManager.Play(obstacleSurpassed10Sound);
                LeanTween.scale(gameObject, transform.localScale * 1.5f, 0.2f).setEase(LeanTweenType.easeOutCirc);
                LeanTween.scale(gameObject, Vector3.one, 0.2f).setDelay(0.2f).setEase(LeanTweenType.easeInCirc);
            }
            else
            {

                AudioManager.Play(obstacleSurpassedSound);
                LeanTween.scale(gameObject, transform.localScale * 1.1f, 0.15f).setEase(LeanTweenType.easeOutCirc);
                LeanTween.scale(gameObject, Vector3.one, 0.15f).setDelay(0.15f).setEase(LeanTweenType.easeInCirc);
            }
        }
    }
}