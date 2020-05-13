using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AudioFade
{
    public static event Action OnFadeInEnd = delegate{};
    public static event Action OnFadeOutEnd = delegate {};

    public static IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;
 
        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
        OnFadeInEnd?.Invoke();
        source.Stop();
        source.volume = startVolume;

    }

    public static IEnumerator FadeIn(AudioSource source,float fadeTime, float MaxVolume)
    {
        float startVolume = 0.2f;
 
        source.volume = 0;
        source.Play();
 
        while (source.volume < MaxVolume/100)
        {
            source.volume += startVolume * Time.deltaTime / fadeTime;
 
            yield return null;
        }
        OnFadeOutEnd?.Invoke();
        source.volume = MaxVolume/100;
    }
}