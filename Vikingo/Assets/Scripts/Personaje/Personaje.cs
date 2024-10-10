using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    public PersonajeExperiencia PersonajeExperiencia {  get; private set; }// a�adir la experiencia adquirido por las misiones

    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }

    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();// exp
    }

    public void RestaurarPersonaje()
    {
        PersonajeVida.RestaurarPersonaje();
        PersonajeMana.RestablecerMana();
        PersonajeAnimaciones.RevivirPersonaje();
    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if(stats.PuntosDisponibles <= 0)
        {
            return;
        }

        switch (tipo) 
        { 
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.A�adirBonusPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.A�adirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                stats.A�adirBonusPorAtributoDestreza();
                break;
        }

        stats.PuntosDisponibles -= 1;
    }

    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }
}