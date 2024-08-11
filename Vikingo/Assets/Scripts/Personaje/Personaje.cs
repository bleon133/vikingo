using UnityEngine;

public class Personaje : MonoBehaviour
{
    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje();
        PersonajeMana.RestablecerMana();
        PersonajeAnimaciones.RevivirPersonaje();
    }
}