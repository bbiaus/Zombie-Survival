using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


  

public class ToggleSceneSelector : MonoBehaviour
{   
    public static ToggleSceneSelector Instance;
    public Toggle toggleScene1; 
    public Toggle toggleScene2; 
    public Toggle toggleScene3;    
     public Button startButton;  
     
     

    private string selectedScene; // Nombre de la escena seleccionada
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
    void Start()
    {
        // Configurar listeners para los toggles
        toggleScene1.onValueChanged.AddListener(delegate { OnToggleChanged(toggleScene1, "BorisScene"); });
        toggleScene2.onValueChanged.AddListener(delegate { OnToggleChanged(toggleScene2, "DafScene"); });
        toggleScene3.onValueChanged.AddListener(delegate { OnToggleChanged(toggleScene3, "MainScene"); });

        // Asegurar que inicialmente no se pueda presionar el botón "Start"
        startButton.interactable = false;

        // Opcional: Configurar un toggle predeterminado si lo deseas
        if (toggleScene1.isOn)
        {
            selectedScene = "BorisScene";
            startButton.interactable = true;
        }
        
        
    }
    
     

    // Método que se llama cuando cambia el estado de un toggle
    void OnToggleChanged(Toggle changedToggle, string sceneName)
    {
        if (changedToggle.isOn)
        {
            selectedScene = sceneName;

            // Asegurarse de que solo un toggle esté activo
            if (changedToggle != toggleScene1) toggleScene1.isOn = false;
            if (changedToggle != toggleScene2) toggleScene2.isOn = false;
            if (changedToggle != toggleScene3) toggleScene3.isOn = false;

            // Habilitar el botón "Start"
            startButton.interactable = true;
        }
    }

    // Método llamado al presionar el botón "Start"
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(selectedScene))
        {
            PlayerPrefs.SetString("SelectedScene", selectedScene); //guarda escena
            Debug.Log("seguardo la escena");
            SceneManager.LoadScene(selectedScene); // Cargar la escena seleccionada
        }
        else
        {
            Debug.LogError("No se ha seleccionado ninguna escena."); // Depuración en caso de error
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void TriggerGameOver()
    {   
        
        Time.timeScale = 0;
        SceneManager.LoadScene("GameOver");
    }

    
}
