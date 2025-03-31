using UnityEngine;

public class AmmoPickupHandler : MonoBehaviour
{
    [SerializeField] private Gun gunScript;  // Referencia al script Gun del jugador

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("AmmoPickup"))
        {
            if (hit.gameObject != null)  // Verifica que el objeto no haya sido destruido ya
            {
                PickUpAmmo();
                Destroy(hit.gameObject);
            }
        }
    }

    private void PickUpAmmo()
    {
        if (gunScript != null)
        {
            gunScript.AddAmmoClip();  // Llamar al método público para agregar un cargador
        }
    }
}
