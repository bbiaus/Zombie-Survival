using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupSoundPlayer : MonoBehaviour
{
    public AudioClip pickupClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayPickupSound()
    {
        if (pickupClip != null)
        {
            audioSource.PlayOneShot(pickupClip);
        }
    }
}

