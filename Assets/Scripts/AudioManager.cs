using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ID
{
    public class AudioManager : MonoBehaviour
    {

        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AudioManager>();
                }

                return instance;
            }
        }
        
    #region References

    [Header("Fade")]
    [SerializeField]
    private float fadeInSpeed = 0.6f;
    [SerializeField]
    private float fadeOutSpeed = 0.3f;

    [Header("Volume")]

    [SerializeField]
    private float masterVolume = 100;

    public float MasterVolume { get { return masterVolume; } set { masterVolume = value; }}
    [SerializeField]
    private float musicVolume = 100;

    public float MusicVolume{ get { return musicVolume; } set {musicVolume = value; } }

    [SerializeField]
    private float sfxVolume = 100;
    
    public float SFXVolume{ get { return sfxVolume; } set { sfxVolume = value; }}

    [SerializeField]
    private float uiVolume = 100;
    public float UIVolume { get { return uiVolume; } set { uiVolume = value; }}
    
    [SerializeField]
    private float volumeThreshold = -80.0f;

    Audio[] uiAudio;

    Audio[] musicAudio;

    Audio[] sfxAudio;

    GameObject musicGameObject;


    GameObject SFXGameObject;


    GameObject UIGameObject;

    public AudioMixer mixer;

    #endregion

    #region Unity Methods
        private void Awake()
        {
            if(instance != this && instance != null)
            {
                Destroy(gameObject);
                return;

            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            musicGameObject = transform.Find("MusicHolder").gameObject;
            SFXGameObject = transform.Find("SFXHolder").gameObject;
            InitMusic();
        }
        #endregion


    #region static methods
        public static void PlayOnce(string name)
        {
            Instance.PlayOnce(Instance.FindAudio(name));
        }
        public static void Play(string name)
        {
            Instance.Play(Instance.FindAudio(name));
        }

        public static void Stop(string name)
        {
            Instance.Stop(Instance.FindAudio(name));
        }


        public static void FadeIn(string name)
        {
            Instance.FadeIn(Instance.FindAudio(name));
        }

        public static void FadeOut(string name)
        {
            Instance.FadeOut(Instance.FindAudio(name));
        }

        public static void Pause(string name)
        {
            Instance.PauseSound(Instance.FindAudio(name));
        }

        public static void Resume(string name)
        {
            Instance.ResumeSound(Instance.FindAudio(name));
        }

        public static void PlayDelayed(string name, float delay)
        {
            Instance.PlayDelayed(Instance.FindAudio(name), delay);
        }


    #endregion

    #region public Methods

        void PlayOnce(Audio sound)
        {
            sound.PlayOnce();
        }

        void Play(Audio sound)
        {
            sound.Play();
        }
        void PlayDelayed(Audio sound, float delay)
        {
            sound.PlayDelayed(delay);
        }

        void Stop(Audio sound)
        {
            sound.Stop();
        }

        void PauseSound(Audio sound)
        {
            sound.Pause();
        }

        void ResumeSound(Audio sound)
        {
            sound.Resume();
        }
        void FadeOut(Audio audio)
        {
            StartCoroutine(AudioFade.FadeOut(audio.source, fadeOutSpeed));
        }

        void FadeIn(Audio audio)
        {
            StartCoroutine(AudioFade.FadeIn(audio.source, fadeInSpeed, musicVolume));
        }

        public void SetMasterVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat("MasterVolume", volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat("MasterVol", value);
            }
        }

        public void SetMusicVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat("MusicVol", volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat("MusicVol", value);
            }
        }

        public void SetSFXVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat("SFXVol", volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat("SFXVol", value);
            }
        }

        public void SetUIVolume(float sliderValue)
        {
            if (sliderValue <= 0)
            {
                mixer.SetFloat("UIVol", volumeThreshold);
            }
            else
            {
                // Translate unit range to logarithmic value. 
                float value = 20f * Mathf.Log10(sliderValue);
                mixer.SetFloat("UIVol", value);
            }
        }

        public void ClearMasterVolume()
        {
            mixer.ClearFloat("MasterVol");
        }

        public void ClearMusicVolume()
        {
            mixer.ClearFloat("MusicVol");
        }

        public void ClearSFXVolume()
        {
            mixer.ClearFloat("SFXVol");
        }

        public void ClearUIVolume()
        {
            mixer.ClearFloat("UIVol");
        }

    #endregion

    #region private Methods

        private void InitMusic()
        {

            musicAudio = Resources.LoadAll<Audio>("Audio/Music");

            foreach (var music in musicAudio)
            {
                music.source = musicGameObject.AddComponent<AudioSource>();
                music.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
                music.source.loop = music.loop;
                music.source.volume = music.volume;
                music.source.clip = music.clip;
                music.source.playOnAwake = music.playOnAwake;
                if(music.playOnAwake)
                {
                    music.Play();
                }
            }

            sfxAudio = Resources.LoadAll<Audio>("Audio/SFX");
            foreach (var music in sfxAudio)
            {
                music.source = SFXGameObject.AddComponent<AudioSource>();
                music.source.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
                music.source.loop = music.loop;
                music.source.volume = music.volume;
                music.source.clip = music.clip;
                music.source.playOnAwake = music.playOnAwake;
                if(music.playOnAwake)
                {
                    music.Play();
                }
            }
        
            uiAudio = Resources.LoadAll<Audio>("Audio/UI");

            foreach (var music in uiAudio)
            {
                music.source = UIGameObject.AddComponent<AudioSource>();
                music.source.outputAudioMixerGroup = mixer.FindMatchingGroups("UI")[0];
                music.source.loop = music.loop;
                music.source.volume = music.volume;
                music.source.clip = music.clip;
                music.source.playOnAwake = music.playOnAwake;
                if(music.playOnAwake)
                {
                    music.Play();
                }
            }
        }

    private Audio FindAudio(string name)
    {
        Audio audio = Array.Find(musicAudio, Audio => Audio.name == name);
        if(audio!=null)
        { 
            return audio;
        }

        audio = Array.Find(sfxAudio, Audio => Audio.name == name);
        if(audio!=null)
        {
            return audio;
        }

        audio = Array.Find(uiAudio, Audio => Audio.name == name);
        if(audio!=null)
        {
            return audio;
        }
            
        Debug.LogError("No se ha encontrado el audio: "+name);
        return null;
    }
    #endregion
    }
}