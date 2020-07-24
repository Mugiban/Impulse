using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButton : MonoBehaviour
{ 
    public float scaleInDuration = .2f;
    public float scaleOutDuration = .2f;
    private void OnEnable()
    {
        StartCoroutine(ScaleText());
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        StopAllCoroutines();
    }

    IEnumerator ScaleText()
    {
        while (true)
        {
            LeanTween.scale(gameObject, transform.localScale * 1.15f, scaleInDuration).setEase(LeanTweenType.easeOutSine);
            LeanTween.scale(gameObject, Vector3.one, scaleOutDuration).setDelay(scaleInDuration).setEase(LeanTweenType.easeInSine);
            yield return new WaitForSeconds(scaleInDuration + scaleOutDuration);
        }
    }
}
