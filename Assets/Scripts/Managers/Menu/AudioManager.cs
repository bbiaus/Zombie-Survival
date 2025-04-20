
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
        SliderMusicVolume.onValueChanged.AddListener(SetMusicVolume);
        ToggleMute.onValueChanged.AddListener(SetMute);
    }

    /*public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            audioMixer.GetFloat("MasterVolume", out lastVolume);
            Debug.LogError(lastVolume);
            _savedVolume = lastVolume;
            audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", _savedVolume);
        }
    }*/

    public void SetMute(bool isMuted)
{
    if (isMuted)
    {
        audioMixer.GetFloat("MasterVolume", out lastVolume);
        _savedVolume = lastVolume > -80 ? lastVolume : 0; // Guarda un volumen válido
        audioMixer.SetFloat("MasterVolume", -80); // Silencia
    }
    else
    {
        audioMixer.SetFloat("MasterVolume", _savedVolume); // Restaura el volumen
        // Actualiza el slider para sincronizarlo con el volumen restaurado
        SliderMasterVolume.value = Mathf.Pow(10, _savedVolume / 20);
    }
}

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }


}