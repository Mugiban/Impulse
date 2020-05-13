using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ForceIndicator : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerController.OnForceChanged += UpdateForceText;
    }

    private void OnDisable()
    {
        PlayerController.OnForceChanged -= UpdateForceText;
    }

    void UpdateForceText(float force)
    {
        textMesh.text = force.ToString("0");
    }
}
