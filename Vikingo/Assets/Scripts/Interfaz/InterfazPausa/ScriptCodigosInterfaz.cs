using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptCodigosInterfaz : MonoBehaviour
{
    // Variables para el menú principal
    private bool juegoPausado = false; // Estado de pausa
    public GameObject menuPausa; // Referencia al menú de pausa
    public GameObject scrollObject; // Referencia al pergamino

    // Variables para el control del pergamino
    public Button openScrollButton;    // Botón para abrir el pergamino
    public Button closeScrollButton1;   // Primer botón para cerrar el pergamino
    public Button closeScrollButton2;   // Segundo botón para cerrar el pergamino
    public Animator scrollAnimator;      // Animador del pergamino
    private bool isOpen = false;         // Estado del pergamino (abierto o cerrado)

    void Start()
    {
        scrollObject.SetActive(false);     // El pergamino está cerrado al inicio
        closeScrollButton1.gameObject.SetActive(false); // Desactivar botón de cerrar 1
        closeScrollButton2.gameObject.SetActive(false); // Desactivar botón de cerrar 2

        openScrollButton.onClick.AddListener(OpenScroll); // Asigna el evento al botón de abrir
        closeScrollButton1.onClick.AddListener(CloseScroll); // Asigna el evento al primer botón de cerrar
        closeScrollButton2.onClick.AddListener(CloseScroll); // Asigna el evento al segundo botón de cerrar
    }

    // Método para cambiar de escena
    public void CambiarEscena(string nombreEscena)
    {
        if (juegoPausado)
        {
            ReanudarJuego();
        }

        SceneManager.LoadScene(nombreEscena);
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Método para pausar el juego
    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; // Detiene el tiempo
        menuPausa.SetActive(true); // Muestra el menú de pausa
        scrollObject.SetActive(true); // Activa el pergamino al pausar
        Debug.Log("Juego pausado.");
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1; // Reanuda el tiempo
        menuPausa.SetActive(false); // Oculta el menú de pausa
        scrollObject.SetActive(false); // Desactiva el pergamino al reanudar
        Debug.Log("Juego reanudado.");
    }

    // Métodos para abrir y cerrar el pergamino
    void OpenScroll()
    {
        if (!isOpen) // Solo abre si no está abierto
        {
            isOpen = true;
            scrollObject.SetActive(true); // Asegúrate de que el pergamino esté activo
            scrollAnimator.SetTrigger("Open"); // Reproduce la animación de apertura

            // Activar botones de cerrar y desactivar el botón de abrir
            closeScrollButton1.gameObject.SetActive(true);
            closeScrollButton2.gameObject.SetActive(true);
            openScrollButton.gameObject.SetActive(false);
        }
    }

    void CloseScroll()
    {
        if (isOpen) // Solo cierra si está abierto
        {
            isOpen = false;
            scrollAnimator.SetTrigger("Close"); // Reproduce la animación de cierre

            // Desactivar botones de cerrar y activar el botón de abrir
            closeScrollButton1.gameObject.SetActive(false);
            closeScrollButton2.gameObject.SetActive(false);
            openScrollButton.gameObject.SetActive(true);

            StartCoroutine(DeactivateScrollAfterDelay(1f)); // Llama a la coroutine para desactivar
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false); // Desactiva el pergamino después de un tiempo
        ReanudarJuego(); // Reanuda el juego automáticamente
    }

    // Update se puede usar para comprobar la tecla de pausa
    void Update()
    {
        // Comprobar si se presiona la tecla P para pausar
        if (Input.GetKeyDown(KeyCode.P) && !juegoPausado)
        {
            PausarJuego();
        }
    }
}
