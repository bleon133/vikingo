using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    private void Awake()
    {
        // Asegura que este GameObject no se destruya al cambiar de escena
        DontDestroyOnLoad(this.gameObject);
    }
}
