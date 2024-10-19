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

    private void Start()
    {
        CargarItemEnVenta();
    }

    private void CargarItemEnVenta()
    {
        // Obtiene el nombre de la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            // Filtra por el nombre de la escena
            if (itemsDisponibles[i].NombreEscena == escenaActual)
            {
                ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
                itemTienda.ConfigurarItemVenta(itemsDisponibles[i]);
            }
        }

    }
}