using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmoPerMag = 12;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int totalMags = 3;
    [SerializeField] private int maxMags = 5;

    private void Start()
    {
        currentAmmo = maxAmmoPerMag;
    }

    public bool CanShoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin balas, recarga!");
            return false;
        }
        return true;
    }

    public void ConsumeBullet()
    {
        currentAmmo--;
    }

    public void Reload()
    {
        if (currentAmmo == maxAmmoPerMag || totalMags <= 0)
        {
            Debug.Log("No necesitas recargar o no tienes más cargadores!");
            return;
        }

        totalMags--;
        currentAmmo = maxAmmoPerMag;
        Debug.Log($"Recargaste! Balas: {currentAmmo} | Cargadores restantes: {totalMags}");
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmoPerMag);
    }

    public void AddAmmoClip()
    {
        if (totalMags < maxMags)
        {
            totalMags++;
        }
    }
}
