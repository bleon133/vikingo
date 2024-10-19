using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TiendaManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ItemTienda itemTiendaPrefab;
    [SerializeField] private Transform panelContenedor;

    [Header("Items")]
    [SerializeField] private ItemVenta[] itemsDisponibles;

    private void OnEnable()
    {
        // Suscribirse al evento de carga de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento cuando se desactiva el objeto
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Cargar los items al iniciar la escena actual
        CargarItemEnVenta();
    }

    // Se ejecuta cada vez que una nueva escena ha sido cargada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CargarItemEnVenta();
    }

    private void CargarItemEnVenta()
    {
        // Limpiar el panel antes de cargar nuevos items
        foreach (Transform child in panelContenedor)
        {
            Destroy(child.gameObject);
        }

        // Obtener el nombre de la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;
        Debug.Log(escenaActual);

        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            // Filtrar por el nombre de la escena
            if (itemsDisponibles[i].NombreEscena == escenaActual)
            {
                ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
                itemTienda.ConfigurarItemVenta(itemsDisponibles[i]);
            }
        }
    }
}
