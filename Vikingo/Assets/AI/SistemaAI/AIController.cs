using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Estados")]
    [SerializeField] private AIEstado estadoInicial;
    [SerializeField] private AIEstado estadoDefault;

    [Header("config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private LayerMask personajeLayerMask;

    public Transform PersonajeReferencia { get; set; }
    public AIEstado EstadoActual { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;

    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    private void Start()
    {
        EstadoActual = estadoInicial;

        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        EstadoActual.EjecutarEstado(controller: this);
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
    }

    public void CambiarEstado(AIEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }
}
