using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkModeSwitch : MonoBehaviour
{
	private Slider slider;
	private Image handleImage;
	[SerializeField] private Image backgroundImage;
	public Color blackColor = new Color(0,0,0,1);

	public static event Action<bool> OnDarkMode;
	private void Awake()
	{
		slider = GetComponent<Slider>();
		handleImage = slider.handleRect.GetComponent<Image>();
		slider.onValueChanged.AddListener(ChangeDarkMode);
		ChangeDarkMode(slider.value);
	}

	void ChangeDarkMode(float value)
	{
		if (value == 0)
		{
			//modo blanco
			handleImage.color = Color.white;
			backgroundImage.color = Color.white;
			OnDarkMode?.Invoke(false);
			//
		}
		else
		{
			//modo negro
			handleImage.color = blackColor;
			backgroundImage.color = blackColor;
			OnDarkMode?.Invoke(true);
		}
	}
}
