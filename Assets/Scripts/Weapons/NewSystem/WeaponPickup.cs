using StarterAssets;
using UnityEngine;
using Weapons.NewSystem.Data;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponData weaponToGive;

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

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No se encontró el componente FirstPersonController en el objeto: " + other.name);
        }
    }
}
