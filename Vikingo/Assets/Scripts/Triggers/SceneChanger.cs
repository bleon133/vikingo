using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string sceneName;

    // Coordenadas específicas donde aparecerá el player en la nueva escena
    public Vector3 targetPosition;

    // Referencia al Player que persiste entre escenas
    private GameObject playerInstance;

    private void Start()
    {
        // Buscar el objeto que tiene el tag "Player" en la escena actual (si persiste de la escena anterior)
        playerInstance = GameObject.FindWithTag("Player");

        if (playerInstance != null)
        {
            DontDestroyOnLoad(playerInstance);  // Mantener al player cuando cambias de escena
        }
        else
        {
            Debug.LogError("No se ha encontrado el Player en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Iniciar el proceso de cambiar de escena y mover al player
            StartCoroutine(ChangeSceneAndMovePlayer());
        }
    }

    // Corrutina para manejar el cambio de escena
    private System.Collections.IEnumerator ChangeSceneAndMovePlayer()
    {
        // Cargar la nueva escena
        yield return SceneManager.LoadSceneAsync(sceneName);

        // Después de cargar la nueva escena, buscar de nuevo al player persistente
        playerInstance = GameObject.FindWithTag("Player");

        if (playerInstance != null)
        {
            // Mover al player a la posición objetivo en la nueva escena
            playerInstance.transform.position = targetPosition;
        }
        else
        {
            Debug.LogError("No se encontró el Player en la nueva escena.");
        }
    }
}
