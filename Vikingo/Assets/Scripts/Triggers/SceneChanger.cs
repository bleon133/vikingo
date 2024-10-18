using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string escenaDestino;   // Nombre de la escena a la que se debe ir
    [SerializeField] private Vector2 posicionDestino; // Posición donde el personaje debe aparecer

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entra en la zona es el 'Vikingo' (player)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cambia de escena
            SceneManager.LoadScene(escenaDestino);

            // Subscribirse al evento de cambio de escena para ajustar la posición del personaje
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encuentra al personaje después de cargar la escena
        GameObject personaje = GameObject.FindWithTag("Player");

        if (personaje != null)
        {
            // Teletransportar al personaje a la nueva posición
            personaje.transform.position = posicionDestino;
        }

        // Desuscribirse del evento para evitar que se ejecute de nuevo innecesariamente
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
