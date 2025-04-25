using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


  

public class ToggleSceneSelector : MonoBehaviour
{   
    public static ToggleSceneSelector Instance;
    
    
     
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
             
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    
    public void StartGame()
    {
        string selectedScene = "MainScene";
        SceneManager.LoadScene(selectedScene);
         Cursor.lockState = CursorLockMode.Locked; 
         Cursor.visible = false;
    
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void TriggerGameOver()
    {   
        Debug.Log("TriggerGameOver llamado. Intentando cargar la escena GameOver...");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }
    
}
