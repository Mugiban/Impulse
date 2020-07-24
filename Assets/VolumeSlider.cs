using System;
using System.Collections;
using System.Collections.Generic;
using ID.Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
	public Slider slider;

	public enum VolumeType
	{
		General,
		Music,
		SFX
	}

	public VolumeType volumeType;
	private void Awake()
	{
		slider = GetComponentInChildren<Slider>();
	}

	private void Start()
	{
		UpdateVolume();
	}

	private void OnEnable()
	{
		slider.onValueChanged.AddListener(VolumeChanger);
		UpdateVolume();
	}

	public void UpdateVolume()
	{
		switch (volumeType)
		{
			case VolumeType.General:
				slider.value = AudioManager.Instance.masterVolume;
				break;
			case VolumeType.Music:
				slider.value = AudioManager.Instance.musicVolume;
				break;
			case VolumeType.SFX:
				slider.value = AudioManager.Instance.sfxVolume;
				break;
		}
	}

	private void OnDisable()
	{
		slider.onValueChanged.RemoveListener(VolumeChanger);
	}

	void VolumeChanger(float value)
	{
		switch (volumeType)
		{
			case VolumeType.General:
				AudioManager.SetMasterVolume(value);
				break;
			case VolumeType.Music:
				AudioManager.SetMusicVolume(value);
				break;
			case VolumeType.SFX:
				AudioManager.SetSFXVolume(value);
				break;
		}
	}
	
}
