using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    private Rigidbody rb;
    private BulletPool bulletPool;  // Referencia al pool de balas

    public void Initialize(BulletPool pool)
    {
        bulletPool = pool;  // Guardamos la referencia al BulletPool
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;  // Movimiento hacia adelante
    }

    void OnCollisionEnter(Collision collision)
    {
        // Devolver la bala al pool (sin buscar en la escena)
        bulletPool.ReturnBullet(this);
    }
}
