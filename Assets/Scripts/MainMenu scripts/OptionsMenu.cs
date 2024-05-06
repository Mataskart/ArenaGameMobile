using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;
using Unity.VisualScripting;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public SliderManager musicSlider;
    public CustomDropdown resolutionsDropdown;
    Resolution[] resolutions;
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
        // resolutions = Screen.resolutions;

        // for (int i = 0; i < resolutions.Length; i++)
        // {
        //     string option = resolutions[i].width + " x " + resolutions[i].height;
        //     resolutionsDropdown.CreateNewItem(option, null);

        //     if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        //     {
        //         resolutionsDropdown.ChangeDropdownInfo(i);
        //     }
        // }

        // resolutionsDropdown.SetupDropdown();
        // resolutionsDropdown.onValueChanged.AddListener(setResolution);
    }

    public void setResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
