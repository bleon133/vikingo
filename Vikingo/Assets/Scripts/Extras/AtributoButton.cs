using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeraci�n que define los tipos de atributos que un personaje o entidad puede tener
public enum TipoAtributo
{
    Fuerza,       // Representa la fuerza f�sica
    Inteligencia, // Representa la capacidad intelectual o m�gica
    Destreza      // Representa la agilidad o precisi�n
}

// Clase que maneja el comportamiento de los botones de atributos
public class AtributoButton : MonoBehaviour
{
    // Evento est�tico que notifica cuando se agrega un atributo
    // Utiliza una acci�n gen�rica que acepta un TipoAtributo como par�metro
    public static Action<TipoAtributo> EventoAgregarAtributo;

    // Variable serializable que define el tipo de atributo asociado a este bot�n en el inspector de Unity
    [SerializeField] private TipoAtributo tipo;

    // M�todo que se llama cuando el usuario hace clic en el bot�n para agregar el atributo
    public void AgregarAtributo()
    {
        // Si el evento EventoAgregarAtributo tiene suscriptores, se dispara el evento
        // Se pasa el tipo de atributo asociado a este bot�n como par�metro
        EventoAgregarAtributo?.Invoke(tipo);
    }
}
