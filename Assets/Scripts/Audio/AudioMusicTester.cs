using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ID.Audio;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioMusicTester : MonoBehaviour
{
    public Audio currentAudio;
    public Audio nextAudio;
    private Button button;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayAudio);
        closeButton.onClick.AddListener(StopAudio);
        pauseButton.onClick.AddListener(PauseAudio);
    }

    private void Start()
    {
        
        //AudioManager.Play(currentAudio);
    }

    private void OnEnable()
    {
        AudioEvents.OnAudioEndPlaying += OnAudioEndPlaying;
    }

    private void OnDisable()
    {
        AudioEvents.OnAudioEndPlaying -= OnAudioEndPlaying;
    }

    void PlayAudio()
    {
        currentAudio.SetRepeat();
        AudioManager.Play(currentAudio);
        UpdatePauseSprite(false);
    }

    void OnAudioEndPlaying(ExtendedAudioSource source)
    {
        if (source.currentAudio != currentAudio) return;
        UpdatePauseSprite(true);
    }

    private void Update()
    {
        bool isAudioPlaying = AudioManager.IsPlaying(currentAudio);

        if (isAudioPlaying)
        {
            closeButton.gameObject.SetActive(true);
        }
        else
        {
            closeButton.gameObject.SetActive(false);
        }
    }

    void PauseAudio()
    {
        bool paused = AudioManager.TogglePause(currentAudio);
        UpdatePauseSprite(paused);

    }

    void UpdatePauseSprite(bool paused)
    {
        
        if (paused)
        {
            pauseButton.GetComponent<Image>().sprite = playSprite;
        }
        else
        {
            pauseButton.GetComponent<Image>().sprite = pauseSprite;
        }
    }

    void StopAudio()
    {
        AudioManager.Stop(currentAudio);
        
        UpdatePauseSprite(true);
    }
}
