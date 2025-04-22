using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float life = 100;
    public float lifemax = 100;
    public Image healthbar;
    public Text numberlife;

    void Update()
    {
        _interface();
    }

    void _interface()
    {
        healthbar.fillAmount = life / lifemax;
        //numberlife.text="Life: " + life.ToString("f0");
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        Debug.Log("El jugador recibió: " + damage + " de daño y le queda: " + life + " de vida");
        life = Mathf.Clamp(life, 0, lifemax); // Asegura que no baje de 0

        if (life <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        life += amount;
        life = Mathf.Clamp(life, 0, lifemax); // Asegura que no pase el máximo
    }

    void Die()
    {
        Debug.Log("El jugador murió");
        // Acá podrías desactivar movimiento, mostrar pantalla de derrota, etc.
    }
}