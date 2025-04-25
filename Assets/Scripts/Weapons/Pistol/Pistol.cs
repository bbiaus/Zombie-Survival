using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;


public class Pistol : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunAudioSource;
    [SerializeField] private AudioClip gunShotSound;
    //[SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Camera playerCamera;  // Cámara en primera persona
    
    private float nextFireTime = 0f;
    private AmmoManager ammoManager;
    private StarterAssetsInputs input;

    private void Start()
    {
        ammoManager = GetComponent<AmmoManager>();
        input = FindAnyObjectByType<StarterAssetsInputs>();
    }

    private void Update()
    {
         // Solo permite disparar si no está corriendo y el arma terminó de volver a su posición
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            TryShoot();
        }
        
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ammoManager.Reload();
        }
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

    public void TryShoot()
    {
        Bullet bullet = bulletPool.GetBullet();
        if (bullet == null) return;

        bullet.transform.position = firePoint.position; // Asegurar que salga desde el cañón
        bullet.transform.rotation = firePoint.rotation;

        Vector3 shootDirection = GetShootDirection();
        bullet.Shoot(firePoint.position, Quaternion.LookRotation(shootDirection));
    }
}
