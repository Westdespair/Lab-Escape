using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";


    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0.5f);
    }


    void OnDisable() 
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    public void SetMusicVolume(float valueMusic)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(valueMusic) *20);

    }

    public void SetSFXVolume(float valueSFX)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(valueSFX) *20);
        //AudioManager.instance.ActionSFX(0);

    }


}
