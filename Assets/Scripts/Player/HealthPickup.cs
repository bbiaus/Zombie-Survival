using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f;
    [SerializeField] private AudioClip pickupSound; // Clip de sonido

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colisión con: {other.gameObject.name}");

        if (!other.CompareTag("Player")) return;

        Life playerLife = other.GetComponent<Life>();

        if (playerLife != null)
        {
            playerLife.Heal(healAmount);
            Debug.Log($"Jugador curado por {healAmount}. Vida actual: {playerLife.life}");
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