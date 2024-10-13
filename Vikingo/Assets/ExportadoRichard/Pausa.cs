using UnityEngine;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    private bool isPaused = false; // Estado de pausa
    public GameObject pauseMenu; // Referencia al men� de pausa

    void Start()
    {
        // Aseg�rate de que el men� de pausa est� oculto al inicio
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Comprobar si se presiona la tecla P para pausar/despausar
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // M�todo para pausar/despausar el juego
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Detiene el tiempo
            pauseMenu.SetActive(true); // Muestra el men� de pausa
            Debug.Log("Juego pausado.");
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            pauseMenu.SetActive(false); // Oculta el men� de pausa
            Debug.Log("Juego reanudado.");
        }
    }
}
