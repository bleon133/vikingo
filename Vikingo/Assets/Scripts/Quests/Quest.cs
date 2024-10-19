using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado;

    [Header("Info")]
    public string Nombre;
    public string ID; // saber si se completo la mision y entregar
    public int CantidadObjetivo; // cantidad para completar la mision ej 50 enemigo

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    public int RecompensaOro; // recompensa de dinero ganado
    public float RecompensaExp; // experiencia 
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual; // cantidad sumando para llegar al objectivo principal
    [HideInInspector] public bool QuestCompletadoCheck; // ya completo los objectivos

    [Header("Items Requeridos")]
    public List<ItemRequerido> ItemsRequeridos; // lista de ítems requeridos

    [Header("Control de Visibilidad")]
    public List<string> etiquetasParaMostrar; // Etiquetas de objetos a mostrar cuando se complete la misión
    public List<string> etiquetasParaOcultar; // Etiquetas de objetos a ocultar cuando se complete la misión

    public void AñadirProgreso(int cantidad)
    {
        foreach (var itemRequerido in ItemsRequeridos)
        {
            // Verificamos la cantidad requerida de cada ítem
            if (itemRequerido.Item != null && Inventario.Instance.GetCantidad(itemRequerido.Item) >= itemRequerido.CantidadRequerida)
            {
                CantidadActual += cantidad;
                verificarQuestCompletado();
                // Eliminamos el ítem requerido del inventario
                Inventario.Instance.RemoverItem(itemRequerido.Item, itemRequerido.CantidadRequerida);
            }
        }
    }

    private void verificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        if (QuestCompletadoCheck)
        {
            return;
        }
        QuestCompletadoCheck = true;
        EventoQuestCompletado?.Invoke(this);

        // Mostrar/ocultar objetos en la escena
        ControladorVisibilidad controlador = new ControladorVisibilidad();
        controlador.MostrarObjetosPorEtiqueta(etiquetasParaMostrar, etiquetasParaOcultar);
    }

    private void OnEnable()
    {
        QuestCompletadoCheck = false;
        CantidadActual = 0;
    }
}

[Serializable]
public class ItemRequerido // Clase para manejar los ítems requeridos
{
    public InventarioItem Item; // ítem que se necesita recoger
    public int CantidadRequerida; // cantidad que se necesita recoger
}

[Serializable]
public class QuestRecompensaItem // objetos recogidos en el juego y que se pueda utilizar
{
    public InventarioItem Item; // que se entrega
    public int Cantidad; // que se le otorga
}
