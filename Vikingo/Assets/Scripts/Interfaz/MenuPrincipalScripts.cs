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

    // M�todo para cambiar de escena
    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena est� vac�o. Por favor, asigna un nombre v�lido en el Inspector.");
        }
    }

    // M�todo para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene la reproducci�n
#else
            Application.Quit();
#endif
    }

    // M�todo para pausar/despausar el juego
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Detiene el tiempo
            // Aqu� puedes mostrar un men� de pausa si lo deseas
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            // Aqu� puedes ocultar el men� de pausa si lo deseas
        }
    }
}
