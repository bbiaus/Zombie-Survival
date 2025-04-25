using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int currentHealth;
    private bool isDead = false;
    public Zombie parentZombie;

    public void ReceiveDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        Debug.Log("Zombie recibió daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            parentZombie.Die();
        }
    }

    public void SetHealth(int value)
    {
        currentHealth = value;
    }
}
