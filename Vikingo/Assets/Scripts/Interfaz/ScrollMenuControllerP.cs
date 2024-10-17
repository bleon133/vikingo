using UnityEngine;
using UnityEngine.UI;

public class ScrollMenuControllerP : MonoBehaviour
{
    public Button openScrollButton;    // Bot�n para abrir el pergamino
    public Button closeScrollButton1;   // Primer bot�n para cerrar el pergamino
    public Button closeScrollButton2;   // Segundo bot�n para cerrar el pergamino
    public GameObject scrollObject;     // Objeto del pergamino
    public Animator scrollAnimator;      // Animador del pergamino

    private bool isOpen = false;         // Estado del pergamino (abierto o cerrado)

    void Start()
    {
        scrollObject.SetActive(true);     // El pergamino est� cerrado al inicio
        closeScrollButton1.gameObject.SetActive(false); // Desactivar bot�n de cerrar 1
        closeScrollButton2.gameObject.SetActive(false); // Desactivar bot�n de cerrar 2

        openScrollButton.onClick.AddListener(OpenScroll); // Asigna el evento al bot�n de abrir
        closeScrollButton1.onClick.AddListener(CloseScroll); // Asigna el evento al primer bot�n de cerrar
        closeScrollButton2.onClick.AddListener(CloseScroll); // Asigna el evento al segundo bot�n de cerrar
    }

    void OpenScroll()
    {
        if (!isOpen) // Solo abre si no est� abierto
        {
            isOpen = true;
            scrollObject.SetActive(true); // Activa el pergamino
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

            // Espera para desactivar el pergamino despu�s de un tiempo
            //StartCoroutine(DeactivateScrollAfterDelay(1f));
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false); // Desactiva el pergamino despu�s de un tiempo
    }
}
