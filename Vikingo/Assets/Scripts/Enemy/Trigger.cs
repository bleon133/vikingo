using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onEnterEvent; // Evento para cuando un player entra en el trigger
    public UnityEvent onExitEvent;  // Evento para cuando un player sale del trigger

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra tiene una etiqueta de "player"
        if (other.CompareTag("Player"))
        {
            // Invoca el evento de entrada
            onEnterEvent.Invoke();

            // Aquí puedes agregar más lógica según lo que necesites hacer cuando un player entre en el trigger
            Debug.Log("Player entró en el área");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica si el objeto que sale tiene una etiqueta de "player"
        if (other.CompareTag("Player"))
        {
            // Invoca el evento de salida
            onExitEvent.Invoke();

            // Aquí puedes agregar más lógica según lo que necesites hacer cuando un player salga del trigger
            Debug.Log("Player salió del área");
        }
    }
}