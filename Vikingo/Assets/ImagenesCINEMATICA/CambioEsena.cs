using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEsena : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string nombreEscena;

    // Método que puedes enlazar al botón desde el inspector de Unity
    public void CambiarAScena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}