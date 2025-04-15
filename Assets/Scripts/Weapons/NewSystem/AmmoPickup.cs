using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int magsToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}");

        if (!other.CompareTag("Player")) return;

        var bridgeWeaponController = other.GetComponent<BridgeWeaponController>();
        if (bridgeWeaponController != null)
        {
            bridgeWeaponController.AddAmmo(magsToAdd);
            Destroy(gameObject);
        }
    }


}
