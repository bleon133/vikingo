using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array de objetos que se pueden generar
    public int numberOfObjectsToSpawn = 5; // N�mero de objetos a generar
    public Vector2 spawnAreaMin; // Esquina inferior izquierda del �rea de generaci�n
    public Vector2 spawnAreaMax; // Esquina superior derecha del �rea de generaci�n
    public float minDistance = 1f; // Distancia m�nima entre objetos

    void Start()
    {
        // Llama al m�todo para generar objetos al iniciar la escena
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Repite el proceso de generaci�n para el n�mero especificado de objetos
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector2 spawnPosition; // Posici�n donde se intentar� generar un objeto
            bool validPosition = false; // Bandera para comprobar si se encontr� una posici�n v�lida
            int attempts = 0; // Contador de intentos para encontrar una posici�n v�lida

            // Intenta encontrar una posici�n v�lida hasta 100 veces
            while (!validPosition && attempts < 100)
            {
                // Genera una posici�n aleatoria dentro del �rea definida
                spawnPosition = new Vector2(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y)
                );

                // Verifica si la posici�n generada es v�lida
                if (IsPositionValid(spawnPosition))
                {
                    validPosition = true; // Se encontr� una posici�n v�lida
                    // Instancia el objeto aleatorio en la posici�n generada
                    Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawnPosition, Quaternion.identity);
                }

                attempts++; // Incrementa el contador de intentos
            }
        }
    }

    // M�todo para comprobar si una posici�n es v�lida
    bool IsPositionValid(Vector2 position)
    {
        // Devuelve todos los colliders que se superponen con un c�rculo de radio minDistance
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);

        // Iterar sobre los colliders encontrados para verificar si alguno pertenece a un tronco
        foreach (Collider2D collider in colliders)
        {
            // Si el collider tiene la etiqueta "Tronco", entonces no es una posici�n v�lida
            if (collider.CompareTag("Tronco"))
            {
                return false; // Hay colisi�n con un tronco, por lo tanto no es v�lido
            }
        }

        // Si no se encontraron troncos en el �rea, la posici�n es v�lida
        return true;
    }

    void OnDrawGizmos()
    {
        // Establece el color del gizmo a rojo
        Gizmos.color = Color.red;

        // Define las esquinas del �rea de generaci�n
        Vector3 min = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0);
        Vector3 max = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0);

        // Dibuja l�neas para mostrar el �rea de generaci�n
        Gizmos.DrawLine(min, new Vector3(max.x, min.y, 0)); // L�nea inferior
        Gizmos.DrawLine(min, new Vector3(min.x, max.y, 0)); // L�nea izquierda
        Gizmos.DrawLine(new Vector3(max.x, min.y, 0), max); // L�nea derecha
        Gizmos.DrawLine(new Vector3(min.x, max.y, 0), max); // L�nea superior
    }
}
