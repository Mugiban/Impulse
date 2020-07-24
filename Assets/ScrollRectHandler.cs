using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectHandler : MonoBehaviour
{
	private ScrollRect scrollRect;
	private RectTransform rectTransform;
	[SerializeField] private float maxY = 250f;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		scrollRect = GetComponent<ScrollRect>();
	}

	private void Update()
	{
		ClampYAxis();
	}

	private void ClampYAxis()
	{
		var result = new Vector2( rectTransform.anchoredPosition.x,
			Mathf.Clamp(rectTransform.anchoredPosition.y, 0f, maxY));
		rectTransform.anchoredPosition = result;
	}

}
