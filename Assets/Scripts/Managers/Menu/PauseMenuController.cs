using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
   /* void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }*/
    
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
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        AudioListener.pause = false;
        
     
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioListener.pause = true;
     
    }
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
       AudioListener.pause = false;
    }
/*    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioListener.pause = false;
    
    }
    void OnDestroy()
    {
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/
}