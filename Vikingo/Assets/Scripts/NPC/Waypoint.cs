using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos;

    public Vector3[] Puntos => puntos;

    public Vector3 posicionActual { get; set; }
    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        posicionActual = transform.position;
    }

    public Vector3 ObtenerPosicionMovimiento(int index)
    {
        return posicionActual + puntos[index];
    }
    private void OnDrawGizmos()
    {
        if (juegoIniciado == false && transform.hasChanged)

        {
            posicionActual = transform.position;
        }

        if (puntos == null || puntos.Length <= 0)

        {
            return;
        }
        
          for (int i = 0; i < puntos.Length; i++)

          {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center:puntos[i] + posicionActual, radius: 0.5f);


            if (i < puntos.Length - 1)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(puntos[i] + posicionActual, puntos[i + 1] + posicionActual);

            }
          }
    }
   
    
    
}
