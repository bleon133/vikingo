using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Decisiones/Detectar Personaje")]
public class DecisionDetectarPersonaje : AIDecision
{
    public override bool Decidir(AIController controller)
    {
        return DetectarPersonaje(controller);
    }
    
    private bool DetectarPersonaje(AIController controller)
    {

        Collider2D personajeDetectado = Physics2D.OverlapCircle((Vector2)controller.transform.position, controller.RangoDeteccion, (int)controller.PersonajeLayerMask);

        if (personajeDetectado != null)
        {
            controller.PersonajeReferencia = personajeDetectado.transform;
            return true;

        }

        controller.PersonajeReferencia = null;
        return false;
    }
}
