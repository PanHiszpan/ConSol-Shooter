using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    //private float lastSliderValue;
    //Resolution[] resolutions;
    //public Dropdown resolutionDropdown;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume");
        //lastSliderValue = slider.value;

        /*resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();*/
    }

    public void SetVolume(float sliderValue)
    {       
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue)*20);
        AudioListener.volume = sliderValue;
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.Save();
    }

    public void MuteVolume(bool isMuted)
    {
        if (!isMuted)
        {
            AudioListener.pause = true;
            //slider.value = 0;
        }
        else
        {
            AudioListener.pause = false;
            //slider.value = lastSliderValue;
        }
        isMuted = !isMuted;
    }

    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetFloat("QualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }*/

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen!");
    }

    /*public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }*/
}
