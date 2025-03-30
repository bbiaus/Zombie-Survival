using UnityEngine;

    public class SpawnManager : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
