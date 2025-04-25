using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    public ZombieHealth zombieHealth;

    void Awake()
    {
        if (zombieHealth == null)
        {
            zombieHealth = GetComponentInParent<ZombieHealth>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (zombieHealth != null)
        {
            zombieHealth.ReceiveDamage(damage);
        }
        else
        {
            Debug.LogWarning("ZombieHealth no está asignado en " + gameObject.name);
        }
    }
}
