using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicAudioSource;

    public static MusicManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the music manager between scenes
        }

        musicAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicAudioSource.isPlaying && musicAudioSource.clip == clip) return; // Prevent re-playing if same music
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void PauseMusic()
    {
        musicAudioSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicAudioSource.UnPause();
    }

    // Simple method to change the music clip
    public void ChangeMusic(AudioClip newClip)
    {
        PlayMusic(newClip); // Automatically plays the new clip
    }
}