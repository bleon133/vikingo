using UnityEngine;
using UnityEngine.UI;

public class ScrollMenuControllerP : MonoBehaviour
{
    public Button openScrollButton;    // Botón para abrir el pergamino
    public Button closeScrollButton1;   // Primer botón para cerrar el pergamino
    public Button closeScrollButton2;   // Segundo botón para cerrar el pergamino
    public GameObject scrollObject;     // Objeto del pergamino
    public Animator scrollAnimator;      // Animador del pergamino

    private bool isOpen = false;         // Estado del pergamino (abierto o cerrado)

    void Start()
    {
        scrollObject.SetActive(true);     // El pergamino está cerrado al inicio
        closeScrollButton1.gameObject.SetActive(false); // Desactivar botón de cerrar 1
        closeScrollButton2.gameObject.SetActive(false); // Desactivar botón de cerrar 2

        openScrollButton.onClick.AddListener(OpenScroll); // Asigna el evento al botón de abrir
        closeScrollButton1.onClick.AddListener(CloseScroll); // Asigna el evento al primer botón de cerrar
        closeScrollButton2.onClick.AddListener(CloseScroll); // Asigna el evento al segundo botón de cerrar
    }

    void OpenScroll()
    {
        if (!isOpen) // Solo abre si no está abierto
        {
            isOpen = true;
            scrollObject.SetActive(true); // Activa el pergamino
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

            // Espera para desactivar el pergamino después de un tiempo
            //StartCoroutine(DeactivateScrollAfterDelay(1f));
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false); // Desactiva el pergamino después de un tiempo
    }
}
