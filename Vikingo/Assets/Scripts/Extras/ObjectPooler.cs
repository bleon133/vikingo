using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // La cantidad de objetos que serán creados inicialmente en el pool
    [SerializeField] private int cantidadPorCrear;

    // Lista para almacenar los objetos que pertenecen al pool
    private List<GameObject> lista;

    // Objeto que contendrá todas las instancias creadas, para mantener el pool organizado en la jerarquía de Unity
    public GameObject ListaContenedor { get; private set; }

    // Método para crear el pool de objetos
    public void CrearPooler(GameObject objetoPorCrear)
    {
        // Inicializa la lista de objetos
        lista = new List<GameObject>();

        // Crea un contenedor en la jerarquía de Unity que agrupará los objetos del pool
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}");

        // Bucle para crear la cantidad de objetos especificada
        for (int i = 0; i < cantidadPorCrear; i++)
        {
            // Añade una instancia del objeto al pool
            lista.Add(AñadirInstancia(objetoPorCrear));
        }
    }

    // Método privado que crea una instancia del objeto y lo añade al pool
    private GameObject AñadirInstancia(GameObject objetoPorCrear)
    {
        // Instancia un nuevo objeto como hijo del contenedor
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform);

        // Desactiva el objeto para que no esté visible o funcional hasta que sea necesario
        nuevoObjeto.SetActive(false);

        // Devuelve la referencia al objeto recién creado
        return nuevoObjeto;
    }

    // Método para obtener una instancia no activa (disponible) del pool
    public GameObject ObtenerInstancia()
    {
        // Recorre la lista de objetos en el pool
        for (int i = 0; i < lista.Count; i++)
        {
            // Si encuentra un objeto que no está activo, lo devuelve para su uso
            if (!lista[i].activeSelf)
            {
                return lista[i];
            }
        }

        // Si no hay objetos disponibles, devuelve null
        return null;
    }
}
