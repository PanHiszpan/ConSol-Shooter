using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        DontDestroyOnLoad(this);
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        slider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
}