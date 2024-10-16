using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptMenuPrincipal : MonoBehaviour
{
    private bool juegoPausado = false; // Estado de pausa
    public GameObject menuPausa; // Referencia al men� de pausa

    // Propiedad p�blica para acceder al estado de pausa
    public bool JuegoPausado => juegoPausado;

    // M�todo para cambiar de escena
    public void CambiarEscena(string nombreEscena)
    {
        if (juegoPausado)
        {
            ReanudarJuego();
        }

        SceneManager.LoadScene(nombreEscena);
    }

    // M�todo para salir del juego
    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // M�todo para pausar/despausar el juego
    public void AlternarPausa()
    {
        juegoPausado = !juegoPausado;

        if (juegoPausado)
        {
            Time.timeScale = 0; // Detiene el tiempo
            menuPausa.SetActive(true); // Muestra el men� de pausa
            Debug.Log("Juego pausado.");
        }
        else
        {
            Time.timeScale = 1; // Reanuda el tiempo
            menuPausa.SetActive(false); // Oculta el men� de pausa
            Debug.Log("Juego reanudado.");
        }
    }

    // M�todo para pausar el juego desde el men�
    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; // Detiene el tiempo
        menuPausa.SetActive(true); // Muestra el men� de pausa
        Debug.Log("Juego pausado.");
    }

    // M�todo para reanudar el juego
    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1; // Reanuda el tiempo
        menuPausa.SetActive(false); // Oculta el men� de pausa
        Debug.Log("Juego reanudado.");
    }

    // Update se puede usar para comprobar la tecla de pausa
    void Update()
    {
        // Comprobar si se presiona la tecla P para pausar/despausar
        if (Input.GetKeyDown(KeyCode.P))
        {
            AlternarPausa();
        }
    }
}
