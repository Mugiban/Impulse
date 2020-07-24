using System;
using UnityEngine;
using UnityEngine.Audio;

namespace ID.Audio
{
    [RequireComponent(typeof(AudioPool))]
    public class AudioManager : MonoBehaviour
    {
        public AudioMixer masterMixer;
        private static AudioPool pool;
        public static AudioManager Instance { get; private set; }
        public float masterVolume = 1f;
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public float uiVolume = 1f;

        private const float volumeThreshold = -80f;
        private static string MasterVol = "masterVol";
        private static string MusicVol = "musicVol";
        private static string SfxVol = "sfxVol";
        private static string UiVol = "uiVol";

        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            pool = GetComponent<AudioPool>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            SetMasterVolume(masterVolume);
        }

        public static void Play(Audio audio)
        {
            //si el audio se está reproduciendo, lo para y lo vuelve a empezar
            if (IsPlaying(audio))
            {
                Stop(audio);
            }
            audio.Play();
        }

        public static void PlayOnce(Audio audio)
        {
            audio.PlayOnce();
        }

        public static void PlayDelayed(Audio audio, float delay)
        {
            audio.PlayDelayed(delay);
        }

        public static void FadeIn(Audio audio, float duration)
        {           
            if (IsPlaying(audio))
            {
                Stop(audio);
            }
            
            audio.FadeIn(duration);
        }

        public static void FadeOut(Audio audio, float duration)
        {
            ExtendedAudioSource source = pool.IsPlaying(audio);
            if (source != null)
            {
                audio.FadeOut(duration, source);
            }
        }

        public static void CrossFade(Audio fadeOutAudio, Audio fadeInAudio, float fadeOutDuration = 1f, float fadeInDuration = 1f)
        {
            if (pool.IsPlaying(fadeOutAudio))
            {
                FadeOut(fadeOutAudio, fadeOutDuration);
            }
            FadeIn(fadeInAudio, fadeInDuration);
        }
        /// <summary>
        /// returns true if the audio is paused, false otherwise
        /// </summary>
        /// <param name="audio"></param>
        public static bool TogglePause(Audio audio)
        {
            var extendedAudioSource = pool.IsPlaying(audio);
            if (!extendedAudioSource) return false;
            if (extendedAudioSource)
            {
                if (extendedAudioSource.isPaused)
                {
                    Resume(audio, extendedAudioSource);
                    return false;
                }
                
                Pause(audio, extendedAudioSource);
                return true;
            }
            Debug.LogError("No ha podido pausar el audio, no debería pasar.");
            return false;
        }

        public static void Pause(Audio audio, ExtendedAudioSource source)
        {
            audio.Pause(source);
        }

        public static void Resume(Audio audio, ExtendedAudioSource source)
        {
            audio.Resume(source);
        }

        public static void Stop(Audio currentAudio)
        {
            var extendedAudioSource = pool.IsPlaying(currentAudio);
            if (extendedAudioSource)
            {
                currentAudio.Stop(extendedAudioSource);
            }
        }

        public static void SetMasterVolume(float newVolume)
        {
            Instance.ChangeMasterVolume(newVolume);
        }

        public static void SetMusicVolume(float newVolume)
        {
            Instance.ChangeMusicVolume(newVolume);
        }

        public static void SetSFXVolume(float newVolume)
        {
            Instance.ChangeSFXVolume(newVolume);
        }

        public void ChangeMasterVolume(float newMasterVolume)
        {
            masterVolume = newMasterVolume;
            if (masterVolume <= 0)
            {
                masterMixer.SetFloat(MasterVol, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(masterVolume);
                masterMixer.SetFloat(MasterVol, value);
            }
        }

        public void ChangeMusicVolume(float newMusicVolume)
        {
            musicVolume = newMusicVolume;
            if (musicVolume <= 0)
            {
                masterMixer.SetFloat(MusicVol, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(musicVolume);
                masterMixer.SetFloat(MusicVol, value);
            }
        }

        public void ChangeSFXVolume(float newSfxVolume)
        {
            sfxVolume = newSfxVolume;
            if (sfxVolume <= 0)
            {
                masterMixer.SetFloat(SfxVol, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sfxVolume);
                masterMixer.SetFloat(SfxVol, value);
            }
        }

        public void ChangeUIVolume(float newUiVolume)
        {
            uiVolume = newUiVolume;
            if (uiVolume <= 0)
            {
                masterMixer.SetFloat(UiVol, volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(uiVolume);
                masterMixer.SetFloat(UiVol, value);
            }
        }

        public static bool IsPlaying(Audio currentAudio)
        {
            return pool.IsPlaying(currentAudio);
        }
    }
}