using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class AIController : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private AIEstado estadoInicial;
    [SerializeField] private AIEstado estadoDefault;

    [Header("config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float daño;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;


    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoAtaque;


    private float tiempoParaSiguienteAtaque;
    public Transform PersonajeReferencia { get; set; }
    public AIEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float Daño => daño;
    public float RangoDeteccion => rangoDeteccion;

    public float RangoDeAtaque => rangoDeAtaque;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;

    
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
    public void AplicarDañoAlPersonaje(float cantidad)
    {
        float dañoPorRealizar = 0;
        if (Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
    }

    public void CambiarEstado(AIEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidad)
    {
        if(PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidad);
        }
    }

    
    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }
        return false;
    }
    public bool EsTiempoDeAtacar()
    {
        if(Time.time > tiempoParaSiguienteAtaque)
        {
            return true;
        }

        return false;   
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }
    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }
    }
}
