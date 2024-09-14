using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBoton : MonoBehaviour
{
    // Tag que se debe usar para detectar el objeto en el trigger
    [SerializeField] private string targetTag;

    // Eventos que se pueden asignar desde el Inspector
    [SerializeField] private UnityEvent entrar;
    [SerializeField] private UnityEvent salir;
    [SerializeField] private UnityEvent Boton;

    // Indica si el jugador está dentro del trigger
    private bool isInsideTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            /*
            Debug.Log("Es un player");
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Soy player y presione letra e");
            }
            */
            isInsideTrigger = true;
            entrar?.Invoke();
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            isInsideTrigger = false;
            salir?.Invoke();
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Boton?.Invoke();
        }
    }
}