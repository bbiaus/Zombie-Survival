using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colisión con: {other.gameObject.name}");

        if (!other.CompareTag("Player")) return;

        Life playerLife = other.GetComponent<Life>();

        if (playerLife != null)
        {
            playerLife.Heal(healAmount);
            Debug.Log($"Jugador curado por {healAmount}. Vida actual: {playerLife.life}");
            Destroy(gameObject);
        }
    }
}