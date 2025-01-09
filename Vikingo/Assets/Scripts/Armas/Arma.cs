using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoArma

{ 
    Magia,
    Melee
}

[CreateAssetMenu(menuName = "Personaje/Arma")] //Crea el menu de la interfaz de unity que se llama "Personaje/Arma".
public class Arma : ScriptableObject
{
    [Header("Config")]
    public Sprite ArmaIcono; //Aqui se pone el sprite del arma o el icono del arma tipo de lejos.
    public Sprite IconoSkill; //Aqui se pone el icono del arma tipo meele.
    public TipoArma Tipo; //aqui se especifica el tipo de arma que se va a usar.
    public float Daño; //Daño del arma.

    [Header("Arma Magica")]
    public Proyectil ProyectilPrefab;
    public float ManaRequerida; //Si el arma requiere mana o energia.

    [Header("Stats")]
    public float ChanceCritico; //La probabilidad de un golpe critico.
    public float ChanceBloqueo; //La probabilidad de un bloqueo.

}
