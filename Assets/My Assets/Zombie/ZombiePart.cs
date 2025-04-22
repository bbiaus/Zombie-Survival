using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    public ZombieHealth zombieHealth; // Asignalo desde el inspector o buscá con GetComponentInParent

    public void TakeDamage(int amount)
    {
        zombieHealth.ReceiveDamage(amount);
    }
}

