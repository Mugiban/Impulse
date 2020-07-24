using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace ID.Audio
{
    public static class AudioUtilities
    {
        public static float CalculateVolumeValue(this float volume, Slider slider)
        {
            volume /= slider.maxValue;
            return volume;
        }

        public static void SetRepeat(this Audio audio)
        {
            audio.loop = true;
        }
    }
}