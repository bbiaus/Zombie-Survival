using StarterAssets;
using UnityEngine;
using Weapons.NewSystem.Data;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponToGive;
    [SerializeField] private AudioClip pickupSound; // Clip de sonido

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var fpc = other.GetComponent<FirstPersonController>();
        if (fpc != null)
        {
            fpc.AddWeapon(weaponToGive);

            var bridge = other.GetComponent<BridgeWeaponController>();
            if (bridge != null)
            {
                bridge.EquipWeapon(weaponToGive);
            }
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
        else
        {
            Debug.LogWarning("No se encontró el componente FirstPersonController en el objeto: " + other.name);
        }
    }
}
