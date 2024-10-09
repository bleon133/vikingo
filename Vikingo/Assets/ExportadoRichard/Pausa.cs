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

    // Método para pausar/despausar el juego
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Detiene el tiempo
            // Aquí puedes mostrar un menú de pausa si lo deseas
            Debug.Log("Juego pausado.");
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            // Aquí puedes ocultar el menú de pausa si lo deseas
            Debug.Log("Juego reanudado.");
        }
    }
}
