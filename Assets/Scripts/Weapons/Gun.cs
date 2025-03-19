using UnityEngine;
using UnityEngine.InputSystem; // Nuevo sistema de entrada

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Camera playerCamera;  // Cámara en primera persona

    private float nextFireTime = 0f;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime) // Nuevo sistema de entrada
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet bullet = bulletPool.GetBullet();
        if (bullet == null) return; // No dispares si no hay balas

        Vector3 shootDirection = GetShootDirection(); // Obtiene la dirección desde la cámara
        bullet.Shoot(firePoint.position, Quaternion.LookRotation(shootDirection)); // Dispara hacia allí
    }

    private Vector3 GetShootDirection()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Disparo desde el centro de la pantalla
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))  // 100 unidades de distancia
        {
            return (hit.point - firePoint.position).normalized; // La bala va hacia el punto donde impacta el raycast
        }
        return playerCamera.transform.forward; // Si no hay impacto, dispara recto
    }
}
