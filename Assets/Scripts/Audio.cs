using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="New Sound", menuName= "AudioManager/Audio")]
public class Audio : ScriptableObject
{
    [SerializeField]
    public new string name;

    [SerializeField]
    public AudioClip clip;
    
    [SerializeField]
    [Range(0f, 1f)]
    public float volume = 0.5f;

    [SerializeField]
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [SerializeField]
    [Range(0, 0.5f)]
    public float randomVolume = .1f;

    [SerializeField]
    [Range(0, 0.5f)]
    public float randomPitch = .1f;

    public AudioSource source;

    [SerializeField]
    public bool loop = false;

    [SerializeField]
    public bool playOnAwake;

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    } 
        
    public void PlayDelayed(float delay)
    {
        source.PlayDelayed(delay);
    }

    public void PlayOnce()
    {
        source.PlayOneShot(clip);
    }
    
    public void Pause()
    {
        source.Pause();
    }

    public void Resume()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }
}