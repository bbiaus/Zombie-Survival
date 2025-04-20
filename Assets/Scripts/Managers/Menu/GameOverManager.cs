using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button replayButton; //boton replay
    public Button backMenuButton; //boton de menu principal

    void Start()
    {
        replayButton.onClick.AddListener(ReplayGame);
        backMenuButton.onClick.AddListener(BackToMenu);
    }
    public void ReplayGame()
    {
        string replayScene = PlayerPrefs.GetString("SelectedScene", "");
        if(!string.IsNullOrEmpty(replayScene))
        {
            Time.timeScale = 1; 
            SceneManager.LoadScene(replayScene);

        }

    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
