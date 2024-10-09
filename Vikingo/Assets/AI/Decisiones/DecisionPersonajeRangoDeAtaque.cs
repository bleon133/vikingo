using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisiones/Personaje en rango de ataque")]

public class DecisionPersonajeRangoDeAtaque : AIDecision
{
    public override bool Decidir(AIController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(AIController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return false;
        }

        float distancia = (controller.PersonajeReferencia.position -
                           controller.transform.position).sqrMagnitude;
        if (distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2))
        {
            return true;
        }

        return false;
    }
}
