using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingPool : MonoBehaviour
{
    [SerializeField] private ShellPoolData shellPoolData; // Usar ScriptableObject

    private Queue<GameObject> casingPool = new Queue<GameObject>();

    private void Start()
    {
        // Preinstanciar casquillos según el tamaño definido en el ScriptableObject
        for (int i = 0; i < shellPoolData.PoolSize; i++)
        {
            GameObject casing = Instantiate(shellPoolData.CasingPrefab, transform);
            casing.SetActive(false);
            casingPool.Enqueue(casing);
        }
    }

    public GameObject GetCasing(Vector3 position, Quaternion rotation)
    {
        GameObject casing;

        // Obtener un casquillo del pool si hay disponibles
        if (casingPool.Count > 0)
        {
            casing = casingPool.Dequeue();
        }
        else
        {
            // Si no hay en el pool, crear un nuevo casquillo
            casing = Instantiate(shellPoolData.CasingPrefab, transform);
        }

        // Activar y posicionar el casquillo
        casing.transform.position = position;
        casing.transform.rotation = rotation;
        casing.SetActive(true);

        // Desactivar y devolver al pool después de unos segundos
        StartCoroutine(DeactivateCasing(casing, shellPoolData.DeactivateTime));

        return casing;
    }

    private IEnumerator DeactivateCasing(GameObject casing, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Desactivar el casquillo y devolverlo al pool si hay espacio
        casing.SetActive(false);
        if (casingPool.Count < shellPoolData.PoolSize)
        {
            casingPool.Enqueue(casing);
        }
        else
        {
            Destroy(casing); // Si ya hay suficientes, destruir el exceso
        }
    }
}
