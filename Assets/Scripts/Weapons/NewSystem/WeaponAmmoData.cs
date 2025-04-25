using UnityEngine;

[System.Serializable]
public struct WeaponAmmoData
{
    public int currentAmmo;
    public int remainingMags;

    public WeaponAmmoData(int current, int mags)
    {
        currentAmmo = current;
        remainingMags = mags;
    }
}

