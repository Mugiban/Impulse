using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ID.Audio
{
    public class AudioPool : MonoBehaviour
    {
        [Range(5, 60f)] public int maxPoolInstances = 15;


        private static List<ExtendedAudioSource> audioSources;
        private static List<ExtendedAudioSource> activeAudioSources;
        private static Transform trans;

        private void Awake()
        {
            trans = transform;
            audioSources = new List<ExtendedAudioSource>();
            activeAudioSources = new List<ExtendedAudioSource>();
            for (int i = 1; i < maxPoolInstances+1; i++)
            {
                CreateAudioInstance(i);
            }
        }
        
        private static ExtendedAudioSource CreateAudioInstance(int i = 1)
        {
            var go = new GameObject("AudioInstance "+i, typeof(ExtendedAudioSource));
            var source = go.GetComponent<ExtendedAudioSource>();
            go.transform.SetParent(trans);
            audioSources.Add(source);
            go.SetActive(false);
            return source;
        }

        public static ExtendedAudioSource GetAudioSource()
        {
            foreach (var source in audioSources)
            {
                if (!source.isPlaying && source != null)
                {
                    source.gameObject.SetActive(true);
                    activeAudioSources.Add(source);
                    return source;
                }
            }

            var newSource = CreateAudioInstance();
            newSource.gameObject.SetActive(true);
            return newSource;


        }

        public ExtendedAudioSource IsPlaying(Audio currentAudio)
        {
            foreach (var extendedAudioSource in activeAudioSources)
            {
                if (extendedAudioSource.sourceID == currentAudio.ID)
                {
                    if (extendedAudioSource.isPlaying)
                    {
                        return extendedAudioSource;
                    }
                }
            }

            return null;
        }

        public static void ReturnToPool(ExtendedAudioSource extendedAudioSource)
        {
            
            AudioEvents.AudioEndPlaying(extendedAudioSource);
            activeAudioSources.Remove(extendedAudioSource);
            extendedAudioSource.gameObject.SetActive(false);
        }
    }
}