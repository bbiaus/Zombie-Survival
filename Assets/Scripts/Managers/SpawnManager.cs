using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject civilianPrefab;
    public GameObject zombiePrefab;
    public BoxCollider[] civilianSpawnPoints;
    public BoxCollider[] zombieSpawnPoints;

    // Power-Ups
    public GameObject healthPickupPrefab;
    public GameObject ammoPickupPrefab;

    public Transform[] fixedHealthSpawnPoints;
    public Transform[] fixedAmmoSpawnPoints;

    public BoxCollider[] randomItemSpawnAreas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;



        //Power-Ups random por el mapa
        StartCoroutine(SpawnHealthPickupRoutine());
        StartCoroutine(SpawnAmmoPickupRoutine());

        //Power-Ups fijos en el mapa
        SpawnHealthPickup();
        SpawnAmmoPickup();
    }

    public void SpawnWave(int numCivilians, int numZombies)
    {
        // Spawn civiles
        for (int i = 0; i < numCivilians; i++)
        {
            int randomIndex = Random.Range(0, civilianSpawnPoints.Length);
            var randomPosition = GetRandomPosition(zombieSpawnPoints[randomIndex]);
            Instantiate(civilianPrefab, randomPosition, Quaternion.identity);
        }

        // Spawn zombies
        for (int i = 0; i < numZombies; i++)
        {
            int randomIndex = Random.Range(0, zombieSpawnPoints.Length);
            var randomPosition = GetRandomPosition(zombieSpawnPoints[randomIndex]);
            Instantiate(zombiePrefab, randomPosition, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPosition(BoxCollider box)
    { 
        var centerBox = box.transform.TransformPoint(box.center);
        var size = Vector3.Scale(box.size,box.transform.lossyScale) / 2;

        var randomX = Random.Range(-size.x,size.x);
        var randomY = Random.Range(-size.y,size.y);
        var randomZ = Random.Range(-size.z,size.z);

        return centerBox + new Vector3(randomX,randomY,randomZ);
    }

    //Power-Ups
    //spawnear los powerups de vida en las posiciones fijas
    public void SpawnHealthPickup() 
    {
        foreach (Transform point in fixedHealthSpawnPoints)
        {
            Instantiate(healthPickupPrefab, point.position, Quaternion.identity);
        }
    }
    //spawnear los powerups de munición en las posiciones fijas

    public void SpawnAmmoPickup()
    {
        foreach (Transform point in fixedAmmoSpawnPoints)
        {
            Instantiate(ammoPickupPrefab, point.position, Quaternion.identity);
        }
    }

    private IEnumerator SpawnHealthPickupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            SpawnRandomHealthPickup(); // Método para crear el powerup de curación
        }
    }

    private IEnumerator SpawnAmmoPickupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f); // cada 10 segundos
            SpawnRandomAmmoPickup(); // Método para crear el powerup de munición
        }
    }
    // Método para crear el powerup de curación en una posición aleatoria dentro de un área

    private void SpawnRandomHealthPickup()
    {
        int areaIndex = Random.Range(0, randomItemSpawnAreas.Length);
        Vector3 position = GetRandomPosition(randomItemSpawnAreas[areaIndex]);
        Instantiate(healthPickupPrefab, position, Quaternion.identity);
    }
    // Método para crear el powerup de munición en una posición aleatoria dentro de un área
    private void SpawnRandomAmmoPickup()
    {
        int areaIndex = Random.Range(0, randomItemSpawnAreas.Length);
        Vector3 position = GetRandomPosition(randomItemSpawnAreas[areaIndex]);
        Instantiate(ammoPickupPrefab, position, Quaternion.identity);
    }


}
