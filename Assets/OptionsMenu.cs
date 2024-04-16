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
    public AudioMixer audioMixer;
    public CustomDropdown resolutionsDropdown;
    Resolution[] resolutions;
    Sprite sprite = null;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionsDropdown.CreateNewItem(option, sprite);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionsDropdown.ChangeDropdownInfo(i);
            }
        }

        resolutionsDropdown.SetupDropdown();
        resolutionsDropdown.onValueChanged.AddListener(setResolution);
    }

    public void setResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
