using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numeroDeSlots;

    public Personaje Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    public void A�adirItem(InventarioItem itemPorA�adir, int cantidad)
    {
        if (itemPorA�adir == null || cantidad <= 0)
        {
            return;
        }

        // Verificaci�n en caso de tener ya un �tem similar en inventario
        List<int> indexes = VerificarExistencia(itemPorA�adir.ID);
        if (itemPorA�adir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorA�adir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorA�adir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorA�adir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorA�adir.AcumulacionMax;
                            A�adirItem(itemPorA�adir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorA�adir,
                            itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        if (cantidad > itemPorA�adir.AcumulacionMax)
        {
            A�adirItemEnSlotDisponible(itemPorA�adir, itemPorA�adir.AcumulacionMax);
            cantidad -= itemPorA�adir.AcumulacionMax;
            A�adirItem(itemPorA�adir, cantidad);
        }
        else
        {
            A�adirItemEnSlotDisponible(itemPorA�adir, cantidad);
        }
    }

    private List<int> VerificarExistencia(string itemID)
    {
        List<int> indexesDelItem = new List<int>();

        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexesDelItem.Add(i);
                }
            }
        }

        return indexesDelItem;
    }

    private void A�adirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        itemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }
    }

    public void RemoverItem(InventarioItem item, int cantidad)
    {
        if (item == null || cantidad <= 0)
        {
            return;
        }

        List<int> indexes = VerificarExistencia(item.ID);
        foreach (int index in indexes)
        {
            if (itemsInventario[index].Cantidad >= cantidad)
            {
                itemsInventario[index].Cantidad -= cantidad;
                if (itemsInventario[index].Cantidad <= 0)
                {
                    EliminarItem(index);
                }
                return; // Salimos despu�s de remover la cantidad necesaria
            }
        }
    }

    public int GetCantidad(InventarioItem item)
    {
        if (item == null)
        {
            return 0;
        }

        int cantidad = 0;
        List<int> indexes = VerificarExistencia(item.ID);
        foreach (int index in indexes)
        {
            cantidad += itemsInventario[index].Cantidad;
        }

        return cantidad;
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }

        // Copiar el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;

        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        // Borramos Item del slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);
    }

    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }

    private void EquiparItem(int index)
    {
        if(itemsInventario[index] == null)
        {
            return;
        }

        if(itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        if(itemsInventario[index] == null)
        {
            Debug.Log("Es nulo");
            return;
        }

        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            Debug.Log("Es nulo");
            return;
        }

        Debug.Log("No es nulo");
        itemsInventario[index].RemoverItem();
    }

    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
                break;
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
