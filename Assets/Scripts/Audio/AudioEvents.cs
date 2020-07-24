using System;

namespace ID.Audio
{
    public static class AudioEvents
    {
        public static event Action<ExtendedAudioSource> OnAudioEndPlaying;
        public static event Action OnFadeInEnd;
        public static event Action OnFadeOutEnd;
        public static event Action<ExtendedAudioSource> OnPauseAudio;
        public static event Action<ExtendedAudioSource> OnResumeAudio;
        public static void AudioEndPlaying(ExtendedAudioSource source)
        {
            OnAudioEndPlaying?.Invoke(source);
        }

        public static void AudioFadeInEnd()
        {
            OnFadeInEnd?.Invoke();
        }

        public static void AudioFadeOutEnd()
        {
            OnFadeOutEnd?.Invoke();
        }

        public static void PauseAudio(ExtendedAudioSource source)
        {
            OnPauseAudio?.Invoke(source);
        }

        public static void ResumeAudio(ExtendedAudioSource source)
        {
            OnResumeAudio?.Invoke(source);
        }
    }
}
