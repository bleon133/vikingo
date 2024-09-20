using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private string ReconocerTag;

    [SerializeField] private UnityEvent EventoEntrar;
    [SerializeField] private UnityEvent EventoSalir;

    [SerializeField] private GameObject A_D_objeto;

    [SerializeField] private bool ActivadoInicial = false;

    private bool AdentrodelTrigger = false;

    private void Start()
    {
        if (A_D_objeto != null)
        {
            A_D_objeto.SetActive(ActivadoInicial);
        }
        else
        {
            Debug.LogError("Falta asignar un objeto en A_D_objeto.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ReconocerTag))
        {
            AdentrodelTrigger = true;
            EventoEntrar?.Invoke();

            // Intentamos obtener el componente PersonajeVida
            PersonajeVida personajeVida = other.GetComponent<PersonajeVida>();
            if (personajeVida != null)
            {
                personajeVida.RestaurarVida(10); // Regenerar 10 puntos de vida al entrar
                Debug.Log("Vida restaurada al personaje.");
            }
            else
            {
                Debug.LogWarning("El objeto que entró en el trigger no tiene el componente PersonajeVida.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(ReconocerTag))
        {
            AdentrodelTrigger = false;
            EventoSalir?.Invoke();
        }
    }

    private void Update()
    {
        if (AdentrodelTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (A_D_objeto != null)
            {
                A_D_objeto.SetActive(!A_D_objeto.activeSelf);
            }
            else
            {
                Debug.LogError("Falta asignar un objeto en A_D_objeto.");
            }
        }
    }
}