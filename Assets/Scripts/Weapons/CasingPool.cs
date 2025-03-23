using System.Collections.Generic;
using UnityEngine;

public class CasingPool : MonoBehaviour
{
    [SerializeField] private GameObject casingPrefab;
    [SerializeField] private int poolSize = 20; // Definir la cantidad máxima de casquillos

    private Queue<GameObject> casingPool = new Queue<GameObject>();

    private void Start()
    {
        // Preinstanciar casquillos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject casing = Instantiate(casingPrefab, transform);
            casing.SetActive(false); // Desactivarlos
            casingPool.Enqueue(casing);
        }
    }

    public GameObject GetCasing(Vector3 position, Quaternion rotation)
    {
        GameObject casing;

        if (casingPool.Count > 0)
        {
            // Sacar un casquillo del pool
            casing = casingPool.Dequeue();
        }
        else
        {
            // Si el pool está vacío, instanciar un nuevo casquillo
            casing = Instantiate(casingPrefab, transform);
        }

        // Activar y posicionar el casquillo
        casing.transform.position = position;
        casing.transform.rotation = rotation;
        casing.SetActive(true);

        // Desactivar después de unos segundos y devolver al pool
        StartCoroutine(DeactivateCasing(casing, 4f));

        return casing;
    }

    private IEnumerator<WaitForSeconds> DeactivateCasing(GameObject casing, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Desactivar el casquillo y devolverlo al pool si hay espacio
        casing.SetActive(false);
        if (casingPool.Count < poolSize)
        {
            casingPool.Enqueue(casing);
        }
        else
        {
            Destroy(casing); // Si ya hay suficientes, destruir el exceso
        }
    }
}
