using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;  // Punto desde donde se disparan las balas
    public BulletPool bulletPool;  // Referencia al pool de balas
    public float fireRate = 0.2f;  // Tiempo entre disparos
    private float nextFireTime = 0f;  // Tiempo para el próximo disparo permitido

    void Update()
    {
        // Si el jugador mantiene presionado el botón de disparo y ya pasó el tiempo del fireRate
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // Calcula el próximo disparo permitido
            Shoot();
        }
    }

    void Shoot()
    {
        Bullet bullet = bulletPool.GetBullet();  
        if (bullet != null)  // Evita errores si el pool está vacío
        {
            bullet.Shoot(firePoint.position, firePoint.rotation);  // Dispara la bala
        }
    }

}
