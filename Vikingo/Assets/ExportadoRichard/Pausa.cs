using UnityEngine;

public class Pausa : MonoBehaviour
{
    private bool isPaused = false; // Estado de pausa

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
            // Aqu� puedes mostrar un men� de pausa si lo deseas
            Debug.Log("Juego pausado.");
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            // Aqu� puedes ocultar el men� de pausa si lo deseas
            Debug.Log("Juego reanudado.");
        }
    }
}
