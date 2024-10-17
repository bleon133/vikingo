using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptCodigosInterfaz : MonoBehaviour
{
    // Variables para el men� principal
    private bool juegoPausado = false; // Estado de pausa
    public GameObject menuPausa; // Referencia al men� de pausa
    public GameObject scrollObject; // Referencia al pergamino

    // Variables para el control del pergamino
    public Button openScrollButton;    // Bot�n para abrir el pergamino
    public Button closeScrollButton1;   // Primer bot�n para cerrar el pergamino
    public Button closeScrollButton2;   // Segundo bot�n para cerrar el pergamino
    public Animator scrollAnimator;      // Animador del pergamino
    private bool isOpen = false;         // Estado del pergamino (abierto o cerrado)

    void Start()
    {
        scrollObject.SetActive(false);     // El pergamino est� cerrado al inicio
        closeScrollButton1.gameObject.SetActive(false); // Desactivar bot�n de cerrar 1
        closeScrollButton2.gameObject.SetActive(false); // Desactivar bot�n de cerrar 2

        openScrollButton.onClick.AddListener(OpenScroll); // Asigna el evento al bot�n de abrir
        closeScrollButton1.onClick.AddListener(CloseScroll); // Asigna el evento al primer bot�n de cerrar
        closeScrollButton2.onClick.AddListener(CloseScroll); // Asigna el evento al segundo bot�n de cerrar
    }

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

    // M�todo para pausar el juego
    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0; // Detiene el tiempo
        menuPausa.SetActive(true); // Muestra el men� de pausa
        scrollObject.SetActive(true); // Activa el pergamino al pausar
        Debug.Log("Juego pausado.");
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1; // Reanuda el tiempo
        menuPausa.SetActive(false); // Oculta el men� de pausa
        scrollObject.SetActive(false); // Desactiva el pergamino al reanudar
        Debug.Log("Juego reanudado.");
    }

    // M�todos para abrir y cerrar el pergamino
    void OpenScroll()
    {
        if (!isOpen) // Solo abre si no est� abierto
        {
            isOpen = true;
            scrollObject.SetActive(true); // Aseg�rate de que el pergamino est� activo
            scrollAnimator.SetTrigger("Open"); // Reproduce la animaci�n de apertura

            // Activar botones de cerrar y desactivar el bot�n de abrir
            closeScrollButton1.gameObject.SetActive(true);
            closeScrollButton2.gameObject.SetActive(true);
            openScrollButton.gameObject.SetActive(false);
        }
    }

    void CloseScroll()
    {
        if (isOpen) // Solo cierra si est� abierto
        {
            isOpen = false;
            scrollAnimator.SetTrigger("Close"); // Reproduce la animaci�n de cierre

            // Desactivar botones de cerrar y activar el bot�n de abrir
            closeScrollButton1.gameObject.SetActive(false);
            closeScrollButton2.gameObject.SetActive(false);
            openScrollButton.gameObject.SetActive(true);

            StartCoroutine(DeactivateScrollAfterDelay(1f)); // Llama a la coroutine para desactivar
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false); // Desactiva el pergamino despu�s de un tiempo
        ReanudarJuego(); // Reanuda el juego autom�ticamente
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
