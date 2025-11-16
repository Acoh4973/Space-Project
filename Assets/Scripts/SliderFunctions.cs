using UnityEngine;
using UnityEngine.UI;

public class SliderFunctions : MonoBehaviour
{
    public void changeSound(Slider input)
    {
        AudioManager.instance.soundVolume = input.value;
        AudioManager.instance.SaveSound();
    }
    public void changeMusic(Slider input)
    {
        AudioManager.instance.musicVolume = input.value;
        AudioManager.instance.SaveMusic();

    }

}
