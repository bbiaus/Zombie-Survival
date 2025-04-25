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
    [SerializeField] private GameObject AssaultRiflePickupGO;
    private bool riffleUnlocked = false; // Para que solo se active una vez
    [SerializeField] private TextMeshProUGUI unlockMessage;
    [SerializeField] private AudioSource unlockSound;
    [SerializeField] private float messageDuration = 3f;
    [SerializeField] private AudioSource newWaveSound; // Sonido de nueva oleada

    

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
    private void Update()
    {
        if (!riffleUnlocked && civiliansSaved >= 4)
        {
            AssaultRiflePickupGO.SetActive(true);
            riffleUnlocked = true;
            Debug.Log("Rifle de asalto desbloqueado!");
            StartCoroutine(ShowUnlockMessage("Los civiles crearon un arma nueva para obsequiarte! Andá al refugio a buscar tu recompensa."));
        }
    }


    public void StartNewWave()
    {
        if (currentWave > 0)
        {
            newWaveSound.Play(); // Reproduce el sonido de nueva oleada
        }
        currentWave++;
        waveText.text = "Wave: " + currentWave;


        int numCivilians = 1 + currentWave; // Más civiles en cada oleada
        int numZombies = 10 + (currentWave * 3); // Más zombies en cada oleada

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

       public void Died()
    {
        Debug.Log("El jugador murió. Activando Game Over.");

        // Llamar a TriggerGameOver de ToggleSceneSelector
        if (ToggleSceneSelector.Instance != null)
        {
            ToggleSceneSelector.Instance.TriggerGameOver();
        }
    }
    private IEnumerator ShowUnlockMessage(string message)
    {
        unlockMessage.text = message;
        unlockMessage.gameObject.SetActive(true);
        unlockSound.Play();

        yield return new WaitForSeconds(messageDuration);

        unlockMessage.gameObject.SetActive(false);
    }

    /*oid Died()
    {
       ToggleSceneSelector.Instance.TriggerGameOver(); 
        
    }*/
     /*oid Update()
    {
        // Prueba para activar el Game Over con la tecla "j"
        if (Input.GetKeyDown(KeyCode.J))
        {   
            Debug.Log("se presiono la tecla j");
            ToggleSceneSelector.Instance.TriggerGameOver();
        }
    }*/
}
