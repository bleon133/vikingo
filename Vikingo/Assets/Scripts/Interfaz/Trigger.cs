using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private string ReconocerTag;

    [SerializeField] private UnityEvent EventoEntrar;
    [SerializeField] private UnityEvent EventoSalir;
    [SerializeField] private bool ActivadoInicial = false;

    private bool AdentrodelTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ReconocerTag))
        {
            AdentrodelTrigger = true;
            EventoEntrar?.Invoke();
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
}