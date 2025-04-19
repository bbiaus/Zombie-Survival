using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "MainScene"; // Nombre de la escena del juego

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}

