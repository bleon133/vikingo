using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    public float Daño;
    public float Defensa;
    public float Velocidad;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;
}
