using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    private static PersonajeController instancia;

    private void Awake()
    {
        // Si ya existe una instancia, destruye esta nueva
        if (instancia != null && instancia != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Asigna la instancia actual y marca para no destruirla al cambiar de escena
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
