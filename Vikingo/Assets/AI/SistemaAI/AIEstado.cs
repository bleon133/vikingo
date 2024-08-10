using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Estado")]
public class AIEstado : ScriptableObject
{
    public AIAccion[] Acciones;
    public AITransicion[] Transiciones;

    public void EjecutarEstado(AIController controller)
    {
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }

    private void EjecutarAcciones(AIController controller)
    {
        if(Acciones == null || Acciones.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < Acciones.Length; i++)
        {
            Acciones[i].Ejecutar(controller);
        }
    }

    private void RealizarTransiciones(AIController controller)
    {
        if (Transiciones == null || Transiciones.Length <= 0)
        {
            return; 
        }

        for (int i =  0; i < Transiciones.Length; i++)
        {
            bool decisionValor = Transiciones[i].Decision.Decidir(controller);
            if (decisionValor)
            {
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero);

            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso);


            }
        }

    }
}
