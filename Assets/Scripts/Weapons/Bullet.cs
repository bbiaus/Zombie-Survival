using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 4;
    public float bulletSpeed = 20f;
    private Rigidbody rb;
    private BulletPool bulletPool;  // Referencia al pool de balas
    private bool isActive = false;

    public void Initialize(BulletPool pool)
    {
        bulletPool = pool;  // Guardamos la referencia al BulletPool
    }

    public void Shoot(Vector3 newPosition)
    {
        transform.position = newPosition;
        isActive = true;
        Invoke(nameof(ReturnBullet), timeDestroy);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isActive)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;  // Movimiento hacia adelante
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Devolver la bala al pool (sin buscar en la escena)
        ReturnBullet();
    }

    private void ReturnBullet()
    {
        bulletPool.ReturnBullet(this);
        isActive = false;
    }
}
