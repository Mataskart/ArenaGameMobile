using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;
using Unity.VisualScripting;
using System;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public SliderManager musicSlider;
    public CustomDropdown resolutionsDropdown;
    Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private double currentRefreshRate;
    private int currentResolutionIndex = 0;
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
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log(resolutions[i].refreshRateRatio.value);
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + (int)Math.Round(filteredResolutions[i].refreshRateRatio.value) + " Hz";
            resolutionsDropdown.CreateNewItem(resolutionOption, null, false);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.SetupDropdown();
        resolutionsDropdown.ChangeDropdownInfo(currentResolutionIndex);

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void SetWindow()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void SetWindowedFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
