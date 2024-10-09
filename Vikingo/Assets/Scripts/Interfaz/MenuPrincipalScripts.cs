using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalScripts : MonoBehaviour
{
    [SerializeField]
    private string sceneName; // Nombre de la escena a la que cambiar
    private bool isPaused = false; // Estado de pausa

    void Update()
    {
        // Comprobar si se presiona la tecla Escape para pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Método para cambiar de escena
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena está vacío. Por favor, asigna un nombre válido en el Inspector.");
        }
    }

    // Método para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene la reproducción
#else
            Application.Quit();
#endif
    }

    // Método para pausar/despausar el juego
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Detiene el tiempo
            // Aquí puedes mostrar un menú de pausa si lo deseas
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            // Aquí puedes ocultar el menú de pausa si lo deseas
        }
    }
}
