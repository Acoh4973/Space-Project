using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderFunctions : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] AudioMixer mixer;

    [Header("Audio Sliders")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        { LoadVolume(); }
        else
        {
            changeMusic(musicSlider);
            changeSound(sfxSlider);
        }
    }

    public void changeSound(Slider input)
    {
        //AudioManager.instance.soundVolume = input.value;
        //AudioManager.instance.SaveSound();
        float volume = sfxSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void changeMusic(Slider input)
    {
        //AudioManager.instance.musicVolume = input.value;
        //AudioManager.instance.SaveMusic();
        float volume = musicSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        changeMusic(musicSlider);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        changeSound(sfxSlider);
    }

}
