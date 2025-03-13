using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 4;  // Tiempo antes de que la bala se "destruya" (vuelva al pool)
    public float bulletSpeed = 20f;  // Velocidad de la bala
    private Rigidbody rb;
    private BulletPool bulletPool;  // Referencia al pool de balas
    private bool isActive = false;  // Indica si la bala está activa

    // Método para inicializar la bala con el pool al que pertenece
    public void Initialize(BulletPool pool)
    {
        bulletPool = pool;  
    }

    // Método para disparar la bala
    public void Shoot(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;  // Coloca la bala en la posición del disparo
        transform.rotation = newRotation;  // Ajusta la dirección de la bala según la rotación del arma
        isActive = true;  // Marca la bala como activa
        Invoke(nameof(ReturnBullet), timeDestroy);  // Programa la devolución de la bala al pool después de un tiempo
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtiene la referencia al Rigidbody
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;  // Aplica velocidad hacia adelante
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Cuando la bala choca con algo, se devuelve al pool
        ReturnBullet();
    }

    private void ReturnBullet()
    {
        bulletPool.ReturnBullet(this);  // Llama al método de retorno en el pool
        isActive = false;  // Marca la bala como inactiva
    }
}