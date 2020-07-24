using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace ID.Audio
{
    [CreateAssetMenu(fileName = "New Audio", menuName = "Audio/Audio")]
    public class Audio : ScriptableObject
    {
        public AudioClip[] clips;

        public int ID;
        public AudioMixerGroup audioMixer;

        [Range(0f, 1f)] public float volume = .5f;

        [SerializeField] [Range(0, 0.5f)] public float randomVolume = .1f;

        [SerializeField] [Range(0, 2f)] public float pitch = 1f;
        [Range(0f, 1f)] public float randomPitch = .1f;

        [SerializeField] [Range(0, 1f)] public float spatialBlend;

        public bool loop = false;

        public void Play(ExtendedAudioSource source = null)
        {
            //Get audiosource
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }

            source.Setup(this);
            source.Play();
        }
        public void Stop(ExtendedAudioSource source = null)
        {
            //Get audiosource
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }
            source.Stop();
        }
        
        
        public void PlayOnce(ExtendedAudioSource source = null)
        {
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }
            source.Setup(this);
            source.PlayOnce();
        }

        public void PlayDelayed(float delay, ExtendedAudioSource source = null)
        {
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }
            source.Setup(this);
            source.PlayDelayed(delay);
        }
        
        public void Pause(ExtendedAudioSource source = null)
        {
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }

            source.Pause();
        }
        
        public void Resume(ExtendedAudioSource source)
        {
            source.Resume();
        }

        public void FadeIn(float duration, ExtendedAudioSource source = null)
        {
            if (source == null)
            {
                source = AudioPool.GetAudioSource();
            }
            source.Setup(this);
            source.FadeIn(duration);
        }

        public void FadeOut(float duration, ExtendedAudioSource source = null)
        {
            source.FadeOut(duration);
        }
    }
}