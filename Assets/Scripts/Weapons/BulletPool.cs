using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    
    private Queue<Bullet> bulletPool = new Queue<Bullet>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bulletGO = Instantiate(bulletPrefab);
            if (bulletGO.TryGetComponent(out Bullet bullet))
            {
                bullet.Initialize(this);
                bulletGO.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }
    }

    public Bullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            Bullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        // Si no hay en el pool, se crea una nueva
        GameObject bulletGO = Instantiate(bulletPrefab);
        if (bulletGO.TryGetComponent(out Bullet bulletNew))
        {
            bulletNew.Initialize(this);
            return bulletNew;
        }

        return null;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
