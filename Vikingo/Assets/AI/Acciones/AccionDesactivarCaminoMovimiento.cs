using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Acciones/Desactivar Camino Movimiento")]

public class AccionDesactivarCaminoMovimiento : AIAccion
{
    public override void Ejecutar(AIController controller)
    {
        if (controller.EnemigoMovimiento == null)
        {
            return;
        }

        controller.EnemigoMovimiento.enabled = false;
    }
}
