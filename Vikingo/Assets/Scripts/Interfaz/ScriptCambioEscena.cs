using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptCambioEscena : MonoBehaviour
{
    private ScriptMenuPrincipal menuPrincipal; // Referencia al script de men� principal

    void Start()
    {
        // Encuentra el script de men� principal en el objeto que lo contiene
        menuPrincipal = FindObjectOfType<ScriptMenuPrincipal>();
    }

    // M�todo para cambiar de escena
    public void CambiarEscena(string nombreEscena)
    {
        if (menuPrincipal != null)
        {
            // Si est� pausado, reanuda el juego
            if (menuPrincipal.JuegoPausado)
            {
                menuPrincipal.ReanudarJuego();
            }
        }

        SceneManager.LoadScene(nombreEscena);
    }
}
