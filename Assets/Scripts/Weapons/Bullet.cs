using UnityEngine;
using System.Collections;  // Necesario para IEnumerator

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 4f;
    [SerializeField] private float bulletSpeed = 20f;
    
    private Rigidbody rb;
    private BulletPool bulletPool;
    private bool isActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Mejor en Awake que en Start
    }

    public void Initialize(BulletPool pool)
    {
        bulletPool = pool;
    }

    public void Shoot(Vector3 newPosition, Quaternion newRotation)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
        isActive = true;
        rb.linearVelocity = transform.forward * bulletSpeed; // Se aplica velocidad inmediatamente
        StartCoroutine(ReturnBulletAfterTime());
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReturnBullet();
    }

    private IEnumerator ReturnBulletAfterTime()
    {
        yield return new WaitForSeconds(timeDestroy);
        ReturnBullet();
    }

    private void ReturnBullet()
    {
        if (!isActive) return;
        isActive = false;
        rb.linearVelocity = Vector3.zero;  // Resetear velocidad antes de devolver al pool
        bulletPool.ReturnBullet(this);
    }
}
