using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public int poolSize = 20;  // Tamaño inicial del pool
    private Queue<Bullet> bulletPool = new Queue<Bullet>();  // Cola que almacena las balas

    void Start()
    {
        // Inicializar el pool, ahora no hace falta inicializar
        // for (int i = 0; i < poolSize; i++)
        // {
        //     GameObject bullet = Instantiate(bulletPrefab);
        //     bullet.SetActive(false);  // Las balas empiezan desactivadas
        // }
    }

    // Método para obtener una bala del pool
    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)  // Si hay balas disponibles en la cola
        {
            Bullet bullet = bulletPool.Dequeue();  // Saca una bala del pool
            bullet.gameObject.SetActive(true);  // La activa
            return bullet;
        }
        else
        {
            // Si no hay balas en el pool, se crea una nueva
            GameObject bulletGO = Instantiate(bulletPrefab);
            if (bulletGO.TryGetComponent<Bullet>(out Bullet bullet)) // Verifica que tenga el componente Bullet
            {
                bullet.Initialize(this);  // Asigna el pool a la bala
                return bullet;
            }
        }
        return null;  // Si falla, devuelve null
    }

    // Método para devolver una bala al pool
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);  // Desactiva la bala
        bulletPool.Enqueue(bullet);  // La mete de nuevo en la cola
    }
}
