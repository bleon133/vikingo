using System;
using UnityEngine;

public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;
    public bool PuedeSerCurado => Salud < saludMax;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestaurarVida(10);
        }
    }

    public void RestaurarVida(float cantidad)
    {
        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax)
            {
                Salud = saludMax;
            }

            ActualizarBarraVida(Salud, saludMax);
        }
    }

    protected override void PersonajeDerrotado()
    {
        EventoPersonajeDerrotado?.Invoke(); //Si no es nulo se invoca
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        
    }
}
