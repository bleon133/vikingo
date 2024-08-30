using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [SerializeField] private int numeroDeSlots;
    public int NumeroDeSlots => numeroDeSlots;

    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    public void A�adirItem(InventarioItem itemPorA�adir, int cantidad)
    {
        if (itemPorA�adir == null)
        {
            return;
        }

        //Verificaci�n en caso tener ya un item similar en inventario
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
                    }
                }
            }
        }


        if(cantidad <= 0)
        {
            return;
        }

        if(cantidad > itemPorA�adir.AcumulacionMax)
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

        for(int i = 0; i < itemsInventario.Length; i++)
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
                return;
            }
        }
    }
}
