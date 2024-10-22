using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // La cantidad de objetos que ser�n creados inicialmente en el pool
    [SerializeField] private int cantidadPorCrear;

    // Lista para almacenar los objetos que pertenecen al pool
    private List<GameObject> lista;

    // Objeto que contendr� todas las instancias creadas, para mantener el pool organizado en la jerarqu�a de Unity
    public GameObject ListaContenedor { get; private set; }

    // M�todo para crear el pool de objetos
    public void CrearPooler(GameObject objetoPorCrear)
    {
        // Inicializa la lista de objetos
        lista = new List<GameObject>();

        // Crea un contenedor en la jerarqu�a de Unity que agrupar� los objetos del pool
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}");

        // Bucle para crear la cantidad de objetos especificada
        for (int i = 0; i < cantidadPorCrear; i++)
        {
            // A�ade una instancia del objeto al pool
            lista.Add(A�adirInstancia(objetoPorCrear));
        }
    }

    // M�todo privado que crea una instancia del objeto y lo a�ade al pool
    private GameObject A�adirInstancia(GameObject objetoPorCrear)
    {
        // Instancia un nuevo objeto como hijo del contenedor
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform);

        // Desactiva el objeto para que no est� visible o funcional hasta que sea necesario
        nuevoObjeto.SetActive(false);

        // Devuelve la referencia al objeto reci�n creado
        return nuevoObjeto;
    }

    // M�todo para obtener una instancia no activa (disponible) del pool
    public GameObject ObtenerInstancia()
    {
        // Recorre la lista de objetos en el pool
        for (int i = 0; i < lista.Count; i++)
        {
            // Si encuentra un objeto que no est� activo, lo devuelve para su uso
            if (!lista[i].activeSelf)
            {
                return lista[i];
            }
        }

        // Si no hay objetos disponibles, devuelve null
        return null;
    }
}
