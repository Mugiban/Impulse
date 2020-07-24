using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ArrowComponent : MonoBehaviour
{
	private Button arrowButton;
	private LanguageSettings languageSettings;

	public enum ArrowDirection
	{
		LEFT,
		RIGHT
	}

	public ArrowDirection arrowDirection;
	private void Awake()
	{
		languageSettings = GetComponentInParent<LanguageSettings>();
		arrowButton = GetComponent<Button>();
		arrowButton.onClick.AddListener(Change);
	}

	void Change()
	{
		switch (arrowDirection)
		{
			case ArrowDirection.LEFT:
				languageSettings.MoveLeft();
				break;
			case ArrowDirection.RIGHT:
				languageSettings.MoveRight();
				break;
		}
	}
}
