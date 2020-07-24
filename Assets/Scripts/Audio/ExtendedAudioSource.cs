using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ID.Audio
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(AudioSource))]
    public class ExtendedAudioSource : MonoBehaviour
    {
        public AudioSource source;
        public Audio currentAudio;

        [HideInInspector] public int sourceID;
        public bool isPlaying;

        public bool isPaused;
        private void Awake()
        {
            source = GetComponent<AudioSource>();
            sourceID = GetInstanceID();
        }

        private void OnEnable()
        {
            AudioEvents.OnAudioEndPlaying += End;
        }

        private void OnDisable()
        {
            AudioEvents.OnAudioEndPlaying -= End;
        }

        void End(ExtendedAudioSource endSource)
        {
            endSource.Reset();
        }
        public void Update()
        {
            if (isPaused) return;
            
            if (!source.isPlaying)
            {
                AudioPool.ReturnToPool(this);
            }
            else
            {
                isPlaying = true;
            }
        }

        private void Reset()
        {
            if (currentAudio)
            {
                currentAudio.ID = 0;
                fadeInCoroutine = fadeOutCoroutine = null;
            }

            currentAudio = null;
            isPlaying = false;
            isPaused = false;
        }

        public void Setup(Audio newAudio)
        {
            newAudio.ID = sourceID;
            if (newAudio.clips.Length == 0) return;
            currentAudio = newAudio;
            var currentClip = newAudio.clips[Random.Range(0, newAudio.clips.Length)];
            source.clip = currentClip;
            source.volume = newAudio.volume * (1 + Random.Range(-newAudio.randomVolume / 2f, newAudio.randomVolume / 2f));
            source.pitch = newAudio.pitch * (1 + Random.Range(-newAudio.randomPitch / 2f, newAudio.randomPitch / 2f));
            if(newAudio.audioMixer) source.outputAudioMixerGroup = newAudio.audioMixer;
            source.spatialBlend = newAudio.spatialBlend;
            source.loop = newAudio.loop;
        }

        public void Play()
        {
            source.Play();
        }

        public void PlayOnce()
        {
            source.PlayOneShot(source.clip);
        }
        public void PlayDelayed(float delay)
        {
            source.PlayDelayed(delay);
        }
        
        public void Pause()
        {
            if (isPaused) return;
            isPaused = true;
            source.Pause();
        }

        public void Resume()
        {
            if (!isPaused) return;
            isPaused = false;
            source.UnPause();
        }

        public void Stop()
        {
            source.Stop();
            Reset();
        }

        private Coroutine fadeInCoroutine;
        private Coroutine fadeOutCoroutine;
        public void FadeIn(float duration)
        {
            if(fadeInCoroutine!= null) StopCoroutine(fadeInCoroutine);
            if (fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
            
            fadeInCoroutine = StartCoroutine(FadeInCoroutine(duration));
        }

        public void FadeOut(float duration)
        {
            if (fadeInCoroutine != null) StopCoroutine(fadeInCoroutine);
            if (fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
            

            fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(duration));
        }
        
        
        private IEnumerator FadeInCoroutine(float fadeTime = 1f, float MaxVolume = 1f)
        {
            Play();
            source.volume = 0;
            
            while (source.volume < MaxVolume)
            {
                if (!isPaused)
                    source.volume += Time.unscaledDeltaTime / fadeTime;

                if (!source.isPlaying)
                {
                    Debug.Log("source " +source.isPlaying);
                    Reset();
                    yield break;
                }
     
                yield return null;
            }
            source.volume = MaxVolume;
            AudioEvents.AudioFadeInEnd();
        }
        
        

        private IEnumerator FadeOutCoroutine(float fadeTime = 1f)
        {
            while (source.volume > 0)
            {
                if (!isPaused)
                    source.volume -= Time.unscaledDeltaTime / fadeTime;
                
                yield return null;
            }
            
            
            Stop();
            source.volume = 0.0001f;
            AudioEvents.AudioFadeOutEnd();
        }
    }
}