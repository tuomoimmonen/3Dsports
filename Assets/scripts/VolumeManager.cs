using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider musicSlider;

    private void Awake()
    {
 
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", 1f));
            audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", -10f));
            SetSliders();
        }
        else
        {
            SetSliders();
        }
    }

    public void UpdateMusicVolume()
    {
        audioMixer.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    void SetSliders()
    {
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", 1f));
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", -10f));
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", -10f);
    }
}
