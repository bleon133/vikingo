using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject menuPausa; // Asigna el panel del menú de pausa desde el inspector
    private bool juegoPausado = false;

    void Update()
    {
        // Detectar la tecla ESC para pausar/despausar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        menuPausa.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0; // Pausar el tiempo del juego
        juegoPausado = true;
    }

    public void ReanudarJuego()
    {
        menuPausa.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1; // Reanudar el tiempo del juego
        juegoPausado = false;
    }

    public void SalirAlMenu()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo se reanude al salir
        SceneManager.LoadScene("Menu"); // Cambiar a la escena del menú
    }
}