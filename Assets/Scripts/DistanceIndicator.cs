using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DistanceIndicator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerController.OnDistanceSurpassed += UpdateForceText;
        UpdateForceText(0);
    }

    private void OnDisable()
    {
        PlayerController.OnDistanceSurpassed -= UpdateForceText;
    }

    void UpdateForceText(float force)
    {
        textMesh.text = force.ToString("0");
    }
}