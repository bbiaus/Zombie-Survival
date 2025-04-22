using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 3;
    private int currentHealth;
    private bool isDead = false;
    public Zombie parentZombie;

    void Start()
    {
        currentHealth = health;
    }

    public void ReceiveDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        Debug.Log("Zombie recibió daño. Vida restante: " + health);

        if (currentHealth <= 0)
        {
            isDead = true;
            parentZombie.Die();
        }
    }

}
