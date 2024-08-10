using UnityEngine;

public class Personaje : MonoBehaviour
{
    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }

    public void RestaurarPersonaje()
    {

    }
}