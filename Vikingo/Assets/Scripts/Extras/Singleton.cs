using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase gen�rica Singleton que permite crear una instancia �nica de un componente que hereda de MonoBehaviour
// La clase est� restringida para tipos que sean componentes de Unity (es decir, que hereden de Component)
public class Singleton<T> : MonoBehaviour where T : Component
{
    // Variable est�tica para almacenar la instancia �nica de este singleton
    private static T _instance;

    // Propiedad p�blica que devuelve la instancia �nica
    public static T Instance
    {
        get
        {
            // Si la instancia a�n no ha sido creada
            if (_instance == null)
            {
                // Intenta encontrar una instancia del tipo T en la escena
                _instance = FindObjectOfType<T>();

                // Si no encuentra ninguna instancia en la escena
                if (_instance == null)
                {
                    // Crea un nuevo GameObject y le a�ade el componente del tipo T
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();

                    // Cambia el nombre del GameObject al nombre de la clase gen�rica
                    obj.name = typeof(T).Name;
                }
            }
            // Devuelve la instancia �nica
            return _instance;
        }
    }

    // M�todo protegido virtual que puede ser sobrescrito por clases que hereden de esta
    // Es llamado cuando el objeto es inicializado (similar al m�todo Awake en MonoBehaviour)
    protected virtual void Awake()
    {
        // Si la instancia no ha sido asignada todav�a
        if (_instance == null)
        {
            // Asigna la instancia actual (this) al singleton
            _instance = this as T;

            // Marca este objeto para que no sea destruido al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruye este GameObject duplicado
            Destroy(gameObject);
        }
    }
}
