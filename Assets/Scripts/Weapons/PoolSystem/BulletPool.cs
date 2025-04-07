using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private BulletPoolData bulletPoolData; // Usar ScriptableObject en lugar de valores directos

    private Queue<Bullet> bulletPool = new Queue<Bullet>();

    // Singleton para el acceso global al pool de balas
    public static BulletPool Instance { get; private set; }

    private void Start()
    {
        // Preinstanciar las balas según el tamaño definido en el ScriptableObject
        for (int i = 0; i < bulletPoolData.poolSize; i++)
        {
            GameObject bulletGO = Instantiate(bulletPoolData.bulletPrefab);
            if (bulletGO.TryGetComponent(out Bullet bullet))
            {
                bullet.Initialize(this);
                bulletGO.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        GameObject bulletGO = Instantiate(bulletPoolData.bulletPrefab);
        if (bulletGO.TryGetComponent(out Bullet bulletNew))
        {
            bulletNew.Initialize(this);
            bulletNew.gameObject.SetActive(true); // Asegurar que la bala esté activa
            return bulletNew;
        }

        return null;
    }


    public void ReturnBullet(Bullet bullet)
    {
        // Desactivar la bala y devolverla al pool
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
