using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeración que define los tipos de atributos que un personaje o entidad puede tener
public enum TipoAtributo
{
    Fuerza,       // Representa la fuerza física
    Inteligencia, // Representa la capacidad intelectual o mágica
    Destreza      // Representa la agilidad o precisión
}

// Clase que maneja el comportamiento de los botones de atributos
public class AtributoButton : MonoBehaviour
{
    // Evento estático que notifica cuando se agrega un atributo
    // Utiliza una acción genérica que acepta un TipoAtributo como parámetro
    public static Action<TipoAtributo> EventoAgregarAtributo;

    // Variable serializable que define el tipo de atributo asociado a este botón en el inspector de Unity
    [SerializeField] private TipoAtributo tipo;

    // Método que se llama cuando el usuario hace clic en el botón para agregar el atributo
    public void AgregarAtributo()
    {
        // Si el evento EventoAgregarAtributo tiene suscriptores, se dispara el evento
        // Se pasa el tipo de atributo asociado a este botón como parámetro
        EventoAgregarAtributo?.Invoke(tipo);
    }
}
