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
        PlayerController.OnStartDrag += IncreaseScale;
        PlayerController.OnEndDrag += DecreaseScale;
        ResetScale();
    }


    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }
    private void OnDisable()
    {
        PlayerController.OnForceChanged -= UpdateForceText;
        PlayerController.OnStartDrag -= IncreaseScale;
        PlayerController.OnEndDrag -= DecreaseScale;
    }
    
    
    private void DecreaseScale()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.1f);
    }

    private void IncreaseScale()
    {
        LeanTween.scale(gameObject, transform.localScale * 1.1f, 0.1f);
    }

    void UpdateForceText(float force)
    {
        textMesh.text = force.ToString("0");
    }
    

}
