using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int magsToAdd = 1;
    [SerializeField] private AudioClip pickupSound; // Clip de sonido
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}");
        if (!other.CompareTag("Player")) return;
        

        var bridgeWeaponController = other.GetComponent<BridgeWeaponController>();
        if (bridgeWeaponController != null)
        {
            bridgeWeaponController.AddAmmo(magsToAdd);
            if (pickupSound != null)
            {
                GameObject temp = new GameObject("PickupSound");
                AudioSource source = temp.AddComponent<AudioSource>();
                source.clip = pickupSound;
                source.volume = 1f;
                source.Play();
                Destroy(temp, pickupSound.length); // Lo destruye cuando termina el clip
            }
            Destroy(gameObject);
        }
    }


}
