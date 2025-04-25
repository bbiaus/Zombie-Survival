using TMPro;
using UnityEngine;

public class AmmoHUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;
    [SerializeField] private TextMeshProUGUI remainingMagsText;

    public void UpdateAmmo(int current, int max, int mags)
    {
        currentAmmoText.text = current.ToString();
        maxAmmoText.text = "/ " + max.ToString();
        remainingMagsText.text = "Mags: " + mags.ToString();
    }
}
