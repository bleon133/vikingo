using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Acciones/Activar Camino Movimiento")]
public class AccionActivarCaminoMovimiento : AIAccion
{
    public override void Ejecutar(AIController controller)
    {
        if (controller.EnemigoMovimiento == null) 
    
        {
            return;
    
        }

        controller.EnemigoMovimiento.enabled = true;
    }
}
