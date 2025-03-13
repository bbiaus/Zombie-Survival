using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;  // El prefab de la bala
    public int poolSize = 20;  // Cantidad de balas que vamos a preinstanciar
    private Queue<Bullet> bulletPool = new Queue<Bullet>();  // Pool de balas

    void Start()
    {
        // // Inicializar el pool
        // for (int i = 0; i < poolSize; i++)
        // {
        //     GameObject bullet = Instantiate(bulletPrefab);
        //     bullet.SetActive(false);  // Las balas empiezan desactivadas
            
        // }
    }

    // Obtener una bala del pool
    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);  // Activar la bala
            return bullet;
        }
        else
        {
            // Si no hay balas, creamos una nueva
            GameObject bulletGO = Instantiate(bulletPrefab);
            if(bulletGO.TryGetComponent<Bullet>(out Bullet bullet))
            {
                bulletPool.Enqueue(bullet);
                bullet.Initialize(this);
                return bullet;
            }
        }
        return null;
    }

    // Devolver una bala al pool
    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);  // Desactivamos la bala
        bulletPool.Enqueue(bullet);
    }
}

