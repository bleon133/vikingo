using UnityEngine;
using UnityEngine.UI;

public class ScrollMenuController : MonoBehaviour
{
    // Referencia al botón que abrirá el pergamino
    public Button openScrollButton;

    // Referencia al objeto del pergamino
    public GameObject scrollObject;

    // Animador para el pergamino
    public Animator scrollAnimator;

    // Estado del pergamino (abierto o cerrado)
    private bool isOpen = false;

    void Start()
    {
        // Asegurarse de que el pergamino esté cerrado al inicio
        scrollObject.SetActive(false);

        // Añadir el evento al botón
        openScrollButton.onClick.AddListener(ToggleScroll);
    }

    void ToggleScroll()
    {
        // Cambiar el estado y activar/desactivar el pergamino
        isOpen = !isOpen;

        if (isOpen)
        {
            scrollObject.SetActive(true);
            scrollAnimator.SetTrigger("Open");
        }
        else
        {
            scrollAnimator.SetTrigger("Close");
            // Esperar un tiempo antes de desactivar el objeto (ajusta el tiempo según tu animación)
            StartCoroutine(DeactivateScrollAfterDelay(1f));
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false);
    }
}
