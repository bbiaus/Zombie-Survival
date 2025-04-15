using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject civilianPrefab;
    public GameObject zombiePrefab;
    public BoxCollider[] civilianSpawnPoints;
    public BoxCollider[] zombieSpawnPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
}
