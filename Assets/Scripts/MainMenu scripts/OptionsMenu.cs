using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;
using System;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public SliderManager musicSlider;
    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    void Start()
    {

    }
}
