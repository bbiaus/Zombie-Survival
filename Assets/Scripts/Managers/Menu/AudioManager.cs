
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Header("OptionsMenu")]
    public Slider SliderSFXVolume;
    public Slider SliderMasterVolume;
    public Slider SliderMusicVolume;
    public Toggle ToggleMute;
    private float lastVolume = 0;

    private float _savedVolume = 0;
    
    private void Awake()
    {
        SliderSFXVolume.onValueChanged.AddListener(SetSFXVolume);
        SliderMasterVolume.onValueChanged.AddListener(SetMasterVolume);
        ToggleMute.onValueChanged.AddListener(SetMute);
        LoadSettings();

    }
    void Start()
    {
        LoadSettings();
    
    }
    private void LoadSettings()
    {
        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume");
           
            SliderMasterVolume.value = savedMasterVolume; 
            SetMasterVolume(savedMasterVolume);
        }
         if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            
            SliderSFXVolume.value = savedSFXVolume;
            SetSFXVolume(savedSFXVolume);
        }
        
        
         if (PlayerPrefs.HasKey("Mute"))
        {
            bool isMuted = PlayerPrefs.GetInt("Mute") == 1;
          
            ToggleMute.isOn = isMuted;
            SetMute(isMuted);
        }
    }
        private void SaveSettings()
    {
        
        PlayerPrefs.SetFloat("MasterVolume", SliderMasterVolume.value);

        
        PlayerPrefs.SetFloat("SFXVolume", SliderSFXVolume.value);

        PlayerPrefs.SetInt("Mute", ToggleMute.isOn ? 1 : 0);

        PlayerPrefs.Save(); 
    }
  
    public void SetMute(bool isMuted)

    {
        float currentVolume;
        audioMixer.GetFloat("MasterVolume", out currentVolume);

    if (isMuted)
    {
        if (currentVolume > -80)
        {
        
        _savedVolume =  currentVolume > -80 ? currentVolume : 0; // Guarda un volumen válido
        audioMixer.SetFloat("MasterVolume", -80); // Silencia
        Debug.Log("Audio silenciado");
        }
    }
    else
    { 
        if (currentVolume <= -80)
        {
        audioMixer.SetFloat("MasterVolume", _savedVolume);
         float sliderValue = Mathf.Pow(10, _savedVolume / 20); // Calcula el valor del slider
        SliderMasterVolume.value = sliderValue; 
        Debug.Log($"Restaurando volumen: {_savedVolume}, Slider ajustado: {sliderValue}");
        
        }
     
    }
     SaveSettings();
    }


    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        SaveSettings();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SaveSettings();
    }
   

}