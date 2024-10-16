using UnityEngine;
using UnityEngine.UI;

public class ScrollMenuController : MonoBehaviour
{
    // Referencia al bot�n que abrir� el pergamino
    public Button openScrollButton;

    // Referencia al objeto del pergamino
    public GameObject scrollObject;

    // Animador para el pergamino
    public Animator scrollAnimator;

    // Estado del pergamino (abierto o cerrado)
    private bool isOpen = false;

    void Start()
    {
        // Asegurarse de que el pergamino est� cerrado al inicio
        scrollObject.SetActive(false);

        // A�adir el evento al bot�n
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
            // Esperar un tiempo antes de desactivar el objeto (ajusta el tiempo seg�n tu animaci�n)
            StartCoroutine(DeactivateScrollAfterDelay(1f));
        }
    }

    private System.Collections.IEnumerator DeactivateScrollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scrollObject.SetActive(false);
    }
}
