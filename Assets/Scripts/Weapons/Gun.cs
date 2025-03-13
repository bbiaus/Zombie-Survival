using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;  // Punto desde donde se disparan las balas
    public BulletPool bulletPool;  // Referencia al pool de balas
    public float fireRate = 0.1f;  // Tiempo entre disparos
    private float nextFireTime = 0f;  

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Bullet bullet = bulletPool.GetBullet();  
        bullet.Shoot(firePoint.position);
    }
}

