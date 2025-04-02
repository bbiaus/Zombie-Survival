using UnityEngine;

public class CasingEjector : MonoBehaviour
{
    [Header("Casing Settings")]
    [SerializeField] private Transform casingEjectPoint;
    [SerializeField] private CasingPool casingPool;

    public void EjectCasing()
    {
        GameObject casingInstance = casingPool.GetCasing(casingEjectPoint.position, casingEjectPoint.rotation);
        if (casingInstance == null) return;

        Rigidbody casingRb = casingInstance.GetComponent<Rigidbody>();
        Vector3 ejectDirection = casingEjectPoint.right + (Vector3.up * Random.Range(0.2f, 0.8f));

        casingRb.linearVelocity = Vector3.zero;
        casingRb.angularVelocity = Vector3.zero;
        casingRb.AddForce(ejectDirection * Random.Range(1.5f, 2.5f), ForceMode.Impulse);
        casingRb.AddTorque(Random.insideUnitSphere * Random.Range(0.2f, 0.8f), ForceMode.Impulse);
    }
}
