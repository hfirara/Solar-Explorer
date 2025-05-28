using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("music"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("music", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music");
        SFXSlider.value = PlayerPrefs.GetFloat("SFX");

        SetMusicVolume();
        SetSFXVolume();
    }
}
