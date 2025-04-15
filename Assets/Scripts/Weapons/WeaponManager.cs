using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform gunHolder; // Donde se coloca el arma
    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null) Destroy(currentWeapon); // Quita el arma anterior

        currentWeapon = Instantiate(weaponPrefab, gunHolder.position, gunHolder.rotation, gunHolder);
    }
}
