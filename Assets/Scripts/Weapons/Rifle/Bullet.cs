using UnityEngine;
using System.Collections;  // Necesario para IEnumerator

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 4f;
    [SerializeField] private float bulletSpeed = 200f; // Velocidad de las balas


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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            Debug.Log("Colisiono con la Cabeza");
            ZombieHealth zombie = other.GetComponentInParent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.ReceiveDamage(zombie.health); // Muerte instantánea
            }
        }
        else if (other.CompareTag("Body"))
        {
            Debug.Log("Colisiono con el Cuerpo");
            ZombieHealth zombie = other.GetComponentInParent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.ReceiveDamage(1);
            }
        }

        ReturnBullet(); ; // destruir la bala
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
