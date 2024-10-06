using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array de objetos que se pueden generar
    public int numberOfObjectsToSpawn = 5; // Número de objetos a generar
    public Vector2 spawnAreaMin; // Esquina inferior izquierda del área de generación
    public Vector2 spawnAreaMax; // Esquina superior derecha del área de generación
    public float minDistance = 1f; // Distancia mínima entre objetos

    void Start()
    {
        // Llama al método para generar objetos al iniciar la escena
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Repite el proceso de generación para el número especificado de objetos
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector2 spawnPosition; // Posición donde se intentará generar un objeto
            bool validPosition = false; // Bandera para comprobar si se encontró una posición válida
            int attempts = 0; // Contador de intentos para encontrar una posición válida

            // Intenta encontrar una posición válida hasta 100 veces
            while (!validPosition && attempts < 100)
            {
                // Genera una posición aleatoria dentro del área definida
                spawnPosition = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                // Verifica si la posición generada es válida
                if (IsPositionValid(spawnPosition))
                {
                    validPosition = true; // Se encontró una posición válida
                    // Instancia el objeto aleatorio en la posición generada
                    Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnPosition, Quaternion.identity);
                }

                attempts++; // Incrementa el contador de intentos
            }
        }
    }

    // Método para comprobar si una posición es válida
    bool IsPositionValid(Vector2 position)
    {
        // Devuelve todos los colliders que se superponen con un círculo de radio minDistance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);
        return colliders.Length == 0; // Retorna true si no hay colisionadores en el área
    }

    void OnDrawGizmos()
    {
        // Establece el color del gizmo a rojo
        Gizmos.color = Color.red;

        // Define las esquinas del área de generación
        Vector3 min = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0);
        Vector3 max = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0);

        // Dibuja líneas para mostrar el área de generación
        Gizmos.DrawLine(min, new Vector3(max.x, min.y, 0)); // Línea inferior
        Gizmos.DrawLine(min, new Vector3(min.x, max.y, 0)); // Línea izquierda
        Gizmos.DrawLine(new Vector3(max.x, min.y, 0), max); // Línea derecha
        Gizmos.DrawLine(new Vector3(min.x, max.y, 0), max); // Línea superior
    }
}
