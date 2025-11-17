using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Volumes")]
    [Range(0, 1)] public float soundVolume = 1;
    [Range(0, 1)] public float musicVolume = 1;

    private const string SoundKey = "SoundVolume";
    private const string MusicKey = "MusicVolume";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
        ApplyVolumes();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip, soundVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ApplyVolumes()
    {
        sfxSource.volume = soundVolume;
        musicSource.volume = musicVolume;
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat(SoundKey, soundVolume);
        ApplyVolumes();
    }

    public void SaveMusic()
    {
        PlayerPrefs.SetFloat(MusicKey, musicVolume);
        ApplyVolumes();
    }

    void LoadSettings()
    {
        soundVolume = PlayerPrefs.GetFloat(SoundKey, soundVolume);
        musicVolume = PlayerPrefs.GetFloat(MusicKey, musicVolume);
    }
}
