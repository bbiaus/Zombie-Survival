
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClick;
    [SerializeField] private AudioClip _audioSwitch;

    public void ClickAudioOn()
    {
        _audioSource.PlayOneShot(_audioClick);
    }

    public void SwitchAudioOn()
    {
        _audioSource.PlayOneShot(_audioSwitch);
    }
}
