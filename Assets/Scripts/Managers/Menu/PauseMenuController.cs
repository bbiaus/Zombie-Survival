using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsPanelUI;
    private bool isPaused = false;
   
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          if(isPaused)
        
                ResumeGame();
        
        else
        
                PauseGame();
            
        }

       
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        optionsPanelUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        PlayerPrefs.SetInt("Mute", 0);
        PlayerPrefs.Save();
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioListener.pause = true;
        PlayerPrefs.SetInt("Mute", 1);
        PlayerPrefs.Save();
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 
       
       PlayerPrefs.SetInt("Mute", AudioListener.pause ? 1 : 0);
       PlayerPrefs.Save();
        
        SceneManager.LoadScene("MainMenu");
      AudioListener.pause = false;
    }

}