using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int civiliansSaved = 0;
    public int totalZombies = 0;
    public TextMeshProUGUI civiliansSavedText;
    public TextMeshProUGUI waveText;

    private int currentWave = 0;
    private int zombiesRemaining;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartNewWave();
    }

    public void StartNewWave()
    {
        currentWave++;
        waveText.text = "Wave: " + currentWave;

        int numCivilians = 2 + currentWave; // Más civiles en cada oleada
        int numZombies = 3 + (currentWave * 2); // Más zombies en cada oleada

        zombiesRemaining = numZombies;
        SpawnManager.Instance.SpawnWave(numCivilians, numZombies);
    }

    public void CivilianRescued()
    {
        civiliansSaved++;
        civiliansSavedText.text = "Civilians Saved: " + civiliansSaved;
    }

    public void ZombieKilled()
    {
        zombiesRemaining--;

        if (zombiesRemaining <= 0)
        {
            StartCoroutine(NextWaveCountdown());
        }
    }

    private IEnumerator NextWaveCountdown()
    {
        yield return new WaitForSeconds(5f);
        StartNewWave();
    }
}
