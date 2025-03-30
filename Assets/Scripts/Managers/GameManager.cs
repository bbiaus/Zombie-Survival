using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Para manejar la UI
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para acceder desde otros scripts
    private int score = 0;
    private int rescuedCount = 0; // Contador de personajes rescatados
    public TMP_Text civiliansSaved; // UI para mostrar los puntos
    public TMP_Text totalScore;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        Debug.Log("Puntos: " + score);
    }

    public void RescuedCharacter()
    {
        rescuedCount++;
        AddPoints(100);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (civiliansSaved != null)
        {
            civiliansSaved.text = "Civiles Rescatados: " + rescuedCount;
            totalScore.text = "Puntaje total: " + score;
        }
    }
}

