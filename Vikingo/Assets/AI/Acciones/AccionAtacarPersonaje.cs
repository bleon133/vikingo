using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : AIAccion
{
    public override void Ejecutar(AIController controller)
    {
        Atacar(controller);
    }
    private void Atacar(AIController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        }

        if (controller.EsTiempoDeAtacar() == false)
        {
            return; 
        }

        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaque))
        {
            controller.AtaqueMelee(controller.Daño);
            controller.ActualizarTiempoEntreAtaques();
        }

    }

}

