using UnityEngine;

// Enumeración que define los diferentes tipos de ítems que pueden existir en el inventario
public enum TiposDeItem
{
    Armas,       // Ítems relacionados con armas
    Pociones,    // Ítems relacionados con pociones que pueden consumirse
    Pergaminos,  // Ítems relacionados con pergaminos (tal vez mágicos o de información)
    Ingredientes,// Ítems que sirven como ingredientes para crear o combinar otros objetos
    Tesoros,     // Ítems de valor que pueden venderse o recolectarse
    Recursos     // Ítems básicos que sirven para construcción o crafting
}

// Clase base que representa un ítem en el inventario, heredando de ScriptableObject para crear ítems en el editor de Unity
public class InventarioItem : ScriptableObject
{
    // Parámetros generales del ítem
    [Header("Parametros")]
    public string ID;             // Identificador único del ítem
    public string Nombre;         // Nombre del ítem
    public Sprite Icono;          // Ícono visual que representa al ítem
    [TextArea] public string Descripcion; // Descripción detallada del ítem, mostrada en el juego

    // Información adicional sobre el ítem
    [Header("Informacion")]
    public TiposDeItem Tipo;      // Tipo de ítem, basado en la enumeración TiposDeItem
    public bool EsConsumible;     // Define si el ítem se puede consumir (como pociones)
    public bool EsAcumulable;     // Define si el ítem puede apilarse (como recursos o ingredientes)
    public int AcumulacionMax;    // Número máximo de ítems que pueden acumularse en una misma pila

    // Cantidad actual del ítem en el inventario, oculta en el inspector
    [HideInInspector] public int Cantidad;

    // Método para copiar el ítem actual creando una nueva instancia
    public InventarioItem CopiarItem()
    {
        // Se instancia un nuevo objeto del tipo InventarioItem y se retorna
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }

    // Método virtual para usar el ítem. Se puede sobreescribir en clases derivadas
    public virtual bool UsarItem()
    {
        // El ítem puede usarse (retorna true por defecto, pero puede personalizarse en subclases)
        return true;
    }

    // Método virtual para equipar el ítem. Puede ser sobreescrito en clases derivadas
    public virtual bool EquiparItem()
    {
        // El ítem puede equiparse (retorna true por defecto)
        return true;
    }

    // Método virtual para remover el ítem. Puede ser sobreescrito en clases derivadas
    public virtual bool RemoverItem()
    {
        // El ítem puede removerse (retorna true por defecto)
        return true;
    }
}
