using UnityEngine;
using Weapons.NewSystem.Data;
using WeaponSystem;

public class BridgeWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController; // Referencia al controlador de armas

    public void AddAmmo(int amount)
    {
        _weaponController.AddMagazines(amount); // Llama al método para agregar munición
    }
    public void EquipWeapon(WeaponData weapon)
    {
        _weaponController.EquipWeapon(weapon);
    }
}
