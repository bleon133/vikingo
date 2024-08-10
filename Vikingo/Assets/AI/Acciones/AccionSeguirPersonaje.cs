using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : AIAccion
{
    public override void Ejecutar(AIController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(AIController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        
        }
        Vector3 dirHaciaPersonaje = controller.PersonajeReferencia.position - controller.transform.position;
        Vector3 Direccion = dirHaciaPersonaje.normalized;
        float distancia = dirHaciaPersonaje.magnitude;

        if (distancia >= 1.30f)
        {
            controller.transform.Translate(Direccion * controller.VelocidadMovimiento*Time.deltaTime);
        }
    }
}
