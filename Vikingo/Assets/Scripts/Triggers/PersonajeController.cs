using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            // Asigna la instancia actual
            instancia = this;

            // Verifica el nombre de la escena
            string escenaActual = SceneManager.GetActiveScene().name;

            // No aplicar DontDestroyOnLoad si es una escena específica
            if (escenaActual != "Menu" && escenaActual != "Cinematica")
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }

    private void OnEnable()
    {
        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si se ha cargado una escena de "Menu" o "Cinematica", destruir el objeto
        if (scene.name == "Menu" || scene.name == "Cinematica")
        {
            Destroy(this.gameObject);
        }
    }
}
