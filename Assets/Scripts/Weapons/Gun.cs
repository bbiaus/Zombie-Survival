using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Nuevo sistema de entrada

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Camera playerCamera;  // Cámara en primera persona
    [SerializeField] private AudioSource gunAudioSource;  // Sonido del arma
    [SerializeField] private AudioClip gunShotSound;  // Archivo de sonido
    [SerializeField] private ParticleSystem muzzleFlash;  // Efecto de particulas al disparar
    [SerializeField] private Transform gunTransform; // Referencia al objeto del arma
    [SerializeField] private float recoilAmount = 0.1f; // Distancia del retroceso hacia atrás
    [SerializeField] private float recoilSpeed = 5f; // Velocidad con la que vuelve a su posición original
    [SerializeField] private GameObject bulletCasingPrefab; // Prefab del casquillo
    [SerializeField] private Transform casingEjectPoint; // Punto donde se expulsan los casquillos
    [SerializeField] private CasingPool casingPool;  // Referencia al pool de casquillos


    private Vector3 originalPosition; // posición inicial del arma

    private float nextFireTime = 0f;

    private void Start()
    {
        originalPosition = gunTransform.localPosition; // Guarda la posición inicial del arma
    }

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime) // Nuevo sistema de entrada
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private IEnumerator RecoilEffect()
    {
        // Mueve el arma hacia atrás simulando el retroceso
        Vector3 recoilPosition = originalPosition - new Vector3(0, 0, recoilAmount);
        gunTransform.localPosition = recoilPosition;

        // Espera un pequeño tiempo antes de comenzar a volver a la posición original
        yield return new WaitForSeconds(0.05f);

        // Suavemente regresa el arma a su posición original
        float elapsedTime = 0f;
        while (elapsedTime < 0.1f)
        {
            gunTransform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime * recoilSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gunTransform.localPosition = originalPosition;
    }


private void Shoot()
{
    Bullet bullet = bulletPool.GetBullet();
    if (bullet == null) return;

    Vector3 shootDirection = GetShootDirection();
    bullet.Shoot(firePoint.position, Quaternion.LookRotation(shootDirection));

    // Activa el efecto de disparo
    muzzleFlash.Play();

    // Reproduce el sonido de disparo
    gunAudioSource.PlayOneShot(gunShotSound, 0.1f); //sonido en 0.1 para que no moleste mucho

    // Aplica retroceso del arma
    StartCoroutine(RecoilEffect());

    // Expulsa casquillo
    EjectCasing();
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

    

public void EjectCasing()
{
    GameObject casingInstance = casingPool.GetCasing(casingEjectPoint.position, casingEjectPoint.rotation);
    Rigidbody casingRb = casingInstance.GetComponent<Rigidbody>();

    // Calcula la dirección de expulsión con variación
    Vector3 ejectDirection = casingEjectPoint.right + (Vector3.up * Random.Range(0.2f, 0.8f));
    casingRb.linearVelocity = Vector3.zero;  // Resetea la velocidad antes de aplicar fuerza
    casingRb.angularVelocity = Vector3.zero; // Resetea la rotación para evitar giros locos

    // Aplica una fuerza realista
    casingRb.AddForce(ejectDirection * Random.Range(1.5f, 2.5f), ForceMode.Impulse);

    // Aplica torque para que rote de forma variada
    casingRb.AddTorque(Random.insideUnitSphere * Random.Range(0.2f, 0.8f), ForceMode.Impulse);
}

}
