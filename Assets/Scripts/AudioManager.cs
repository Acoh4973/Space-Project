using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource aud;
    [Range(0, 1)] [SerializeField] public float soundVolume;
    [Range(0, 1)][SerializeField] public float musicVolume;
    private const string VolumeKey = "ChosenVolume";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadSettings();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat(VolumeKey, soundVolume);
    }
    public void SaveMusic()
    {
        PlayerPrefs.SetFloat(VolumeKey, musicVolume);
    }

    void LoadSettings()
    {
        soundVolume = PlayerPrefs.GetFloat(VolumeKey, soundVolume);
        musicVolume = PlayerPrefs.GetFloat(VolumeKey, musicVolume);
    }
}
