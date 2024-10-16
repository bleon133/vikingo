using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCambioEscena : MonoBehaviour
{
    private ScriptMenuPrincipal menuPrincipal; // Referencia al script de menú principal

    void Start()
    {
        // Encuentra el script de menú principal en el objeto que lo contiene
        menuPrincipal = FindObjectOfType<ScriptMenuPrincipal>();
    }

    // Método para cambiar de escena
    public void CambiarEscena(string nombreEscena)
    {
        if (menuPrincipal != null)
        {
            // Si está pausado, reanuda el juego
            if (menuPrincipal.JuegoPausado)
            {
                menuPrincipal.ReanudarJuego();
            }
        }

        SceneManager.LoadScene(nombreEscena);
    }
}
