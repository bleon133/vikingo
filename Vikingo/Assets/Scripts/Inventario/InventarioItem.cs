using UnityEngine;

public enum TiposDeItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}

public class InventarioItem : ScriptableObject 
{
    [Header("Parametros")] 
    public string ID;
    public string Nombre;
    public Sprite Icono;
    [TextArea] public string Descripcion;

    [Header("Informacion")]
    public TiposDeItem Tipo;
    public bool EsConsumible;
    public bool EsAcumulable;
    public int AcumulacionMax;

    [HideInInspector] public int Cantidad;
}