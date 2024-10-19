using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVisibilidad
{
    public void MostrarObjetosPorEtiqueta(List<string> etiquetasParaMostrar, List<string> etiquetasParaOcultar)
    {
        // Ocultar los objetos especificados por etiquetas
        foreach (var etiqueta in etiquetasParaOcultar)
        {
            GameObject[] objetos = GameObject.FindGameObjectsWithTag(etiqueta);
            foreach (var objeto in objetos)
            {
                objeto.SetActive(false);
            }
        }

        // Mostrar los objetos especificados por etiquetas
        foreach (var etiqueta in etiquetasParaMostrar)
        {
            GameObject[] objetos = GameObject.FindGameObjectsWithTag(etiqueta);
            foreach (var objeto in objetos)
            {
                objeto.SetActive(true);
            }
        }
    }
}
