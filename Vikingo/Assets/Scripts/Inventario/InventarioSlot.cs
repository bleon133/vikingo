using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Para el uso de TextMeshProUGUI
using UnityEngine;
using UnityEngine.UI; // Para el uso de UI en Unity

// Enumeración que define los tipos de interacciones posibles en un slot de inventario
public enum TipoDeInteraccion
{
    Click,   // Al hacer click en el ítem
    Usar,    // Usar el ítem desde el inventario
    Equipar, // Equipar el ítem
    Remover  // Remover el ítem
}

// Clase que representa un slot (espacio) en el inventario
public class InventarioSlot : MonoBehaviour
{
    // Evento estático que se invoca cuando ocurre una interacción con el slot
    // Recibe el tipo de interacción y el índice del slot
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;

    // Referencia al ícono de la imagen del ítem que aparece en el slot
    [SerializeField] private Image itemIcono;

    // Fondo donde se muestra la cantidad del ítem, si es acumulable
    [SerializeField] private GameObject fondoCantidad;

    // Texto que muestra la cantidad de ítems en el slot (si es acumulable)
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    // Índice del slot en el inventario (posición del ítem)
    public int Index { get; set; }

    // Referencia al componente de botón que permitirá hacer clic en el slot
    private Button _button;

    // Método Awake, se llama al inicializar el objeto. Obtiene el componente Button
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    // Método para actualizar el ítem del slot con el ícono y la cantidad actual
    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        // Asigna el sprite del ícono del ítem al slot
        itemIcono.sprite = item.Icono;

        // Muestra la cantidad del ítem en formato de texto
        cantidadTMP.text = cantidad.ToString();
    }

    // Método para activar o desactivar la UI del slot (ícono y cantidad)
    public void ActivarSlotUI(bool estado)
    {
        // Activa o desactiva la imagen del ítem y el fondo de cantidad
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    // Método llamado cuando el slot es clicado
    public void clickSlot()
    {
        // Lanza el evento de interacción con el tipo "Click" y el índice del slot
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);

        // Si hay un ítem seleccionado previamente para mover (arrastrar), intenta moverlo a este slot
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            // Si el índice de destino es diferente al de origen, se realiza la operación de mover
            if (InventarioUI.Instance.IndexSlotInicialPorMover != Index)
            {
                // Mover el ítem entre los dos slots
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    }

    // Método para seleccionar el botón del slot en la UI
    public void SeleccionarSlot()
    {
        _button.Select();
    }

    // Método para usar el ítem en el slot
    public void SlotUsarItem()
    {
        // Verifica si hay un ítem en el slot, si es así, lanza el evento de uso
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
        }
    }
}
