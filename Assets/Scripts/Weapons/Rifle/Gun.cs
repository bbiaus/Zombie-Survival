using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Nuevo sistema de entrada
using StarterAssets;
//using Unity.VisualScripting;
//using UnityEditor.Rendering;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private CasingPool casingPool;  // Referencia al pool de casquillos
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
    [SerializeField] private Vector3 normalPosition = new Vector3(0.33f, -0.33f, 1f); // Posición normal
    [SerializeField] private Quaternion normalRotation = Quaternion.Euler(0f, -1.79f, 0f);
    [SerializeField] private Vector3 sprintPosition = new Vector3(0.3f, -0.25f, 0.3f); // Posición al correr
    [SerializeField] private Quaternion sprintRotation = Quaternion.Euler(10f, -30f, 0f);
    [SerializeField] private float transitionSpeed = 10f; // Velocidad de interpolación
    [SerializeField] private GameObject crosshair; // Referencia al Crosshair
    [SerializeField] private int maxAmmoPerMag = 30; // Balas por cargador
    [SerializeField] private int currentAmmo; // Balas actuales en el cargador
    [SerializeField] private int totalMags = 3; // Cargadores disponibles
    [SerializeField] private int maxMags = 5; // Límite de cargadores




    private StarterAssetsInputs input; // Referencia al script de inputs
    private Vector3 originalPosition; // posición inicial del arma

    private float nextFireTime = 0f;

    private void Start()
    {
        currentAmmo = maxAmmoPerMag; // Cargar el arma al inicio
        originalPosition = gunTransform.localPosition; // Guarda la posición inicial del arma
        input = FindAnyObjectByType<StarterAssetsInputs>(); // Busca el script de input en la escena
    }

    private bool isReturningFromSprint = false; // Indica si el arma está en transición después de correr

void Update()
{
    bool isSprinting = input.sprint;

    if (Keyboard.current.rKey.wasPressedThisFrame)
    {
        Reload();
    }

    // Si empieza a correr, activar la bandera para bloquear disparo
    if (isSprinting)
    {
        isReturningFromSprint = true;
    }

    // Verificar si el arma llegó a su posición normal
    if (!isSprinting && isReturningFromSprint)
    {
        float distance = Vector3.Distance(gunTransform.localPosition, normalPosition);
        if (distance < 0.01f) // Si ya casi llegó a su posición
        {
            isReturningFromSprint = false; // Ya volvio el arma a su posición normal
        }
    }

    // Solo permite disparar si no está corriendo y el arma terminó de volver a su posición
    if (!isSprinting && !isReturningFromSprint && Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
    {
        nextFireTime = Time.time + fireRate;
        Shoot();
    }

    

    // Transición entre posiciones
    Vector3 targetPosition = isSprinting ? sprintPosition : normalPosition;
    Quaternion targetRotation = isSprinting ? sprintRotation : normalRotation;
    gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, targetPosition, Time.deltaTime * transitionSpeed);
    gunTransform.localRotation = Quaternion.Slerp(gunTransform.localRotation, targetRotation, Time.deltaTime * transitionSpeed);

    // Ocultar HUD al correr o mientras el arma está en transición
    if (crosshair != null)
    {
        crosshair.SetActive(!(isSprinting || isReturningFromSprint)); // si NO esta corriendo o NO esta volviendo del sprint
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

    // Método para disparar la bala
    // Se llama al presionar el botón de disparo
    private void Shoot()
    {

        if (currentAmmo <= 0) //Verifica si hay municion disponible
        {
            Debug.Log("Sin balas, recarga!"); //aca mas adelante voy a agregar sonido, y quizas algun efecto
            return;
        }

        Bullet bullet = bulletPool.GetBullet(); //saco una bala del pool
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

        currentAmmo--; // Restar una bala al disparar
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

    private void Reload() //Método para recargar el arma
    // Se llama al presionar la tecla R
    {
        if (currentAmmo == maxAmmoPerMag || totalMags <= 0)
        {
            Debug.Log("No necesitas recargar o no tienes más cargadores!");
            return;
        }

        int bulletsToReload = maxAmmoPerMag - currentAmmo;
        if (totalMags > 0)
        {
            totalMags--; // Gastar un cargador
            currentAmmo = maxAmmoPerMag; // Recargar al máximo
            Debug.Log("Recargaste! Balas en cargador: " + currentAmmo + " | Cargadores restantes: " + totalMags);
        }
    }
    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmoPerMag);
        Debug.Log($"Munición añadida: {currentAmmo}/{maxAmmoPerMag}");
    }

// En el script Gun
    public void AddAmmoClip()
    {
        if (totalMags < maxMags)
        {
            totalMags++;
            Debug.Log($"Cargadores disponibles: {totalMags}/{maxMags}");
        }
        else
        {
            Debug.Log("Ya tienes el máximo de cargadores.");
        }
    }

    public int GetTotalMags()
    {
        return totalMags;
    }



}
