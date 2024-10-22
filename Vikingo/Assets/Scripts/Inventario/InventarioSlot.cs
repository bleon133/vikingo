using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Para el uso de TextMeshProUGUI
using UnityEngine;
using UnityEngine.UI; // Para el uso de UI en Unity

// Enumeraci�n que define los tipos de interacciones posibles en un slot de inventario
public enum TipoDeInteraccion
{
    Click,   // Al hacer click en el �tem
    Usar,    // Usar el �tem desde el inventario
    Equipar, // Equipar el �tem
    Remover  // Remover el �tem
}

// Clase que representa un slot (espacio) en el inventario
public class InventarioSlot : MonoBehaviour
{
    // Evento est�tico que se invoca cuando ocurre una interacci�n con el slot
    // Recibe el tipo de interacci�n y el �ndice del slot
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;

    // Referencia al �cono de la imagen del �tem que aparece en el slot
    [SerializeField] private Image itemIcono;

    // Fondo donde se muestra la cantidad del �tem, si es acumulable
    [SerializeField] private GameObject fondoCantidad;

    // Texto que muestra la cantidad de �tems en el slot (si es acumulable)
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    // �ndice del slot en el inventario (posici�n del �tem)
    public int Index { get; set; }

    // Referencia al componente de bot�n que permitir� hacer clic en el slot
    private Button _button;

    // M�todo Awake, se llama al inicializar el objeto. Obtiene el componente Button
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    // M�todo para actualizar el �tem del slot con el �cono y la cantidad actual
    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        // Asigna el sprite del �cono del �tem al slot
        itemIcono.sprite = item.Icono;

        // Muestra la cantidad del �tem en formato de texto
        cantidadTMP.text = cantidad.ToString();
    }

    // M�todo para activar o desactivar la UI del slot (�cono y cantidad)
    public void ActivarSlotUI(bool estado)
    {
        // Activa o desactiva la imagen del �tem y el fondo de cantidad
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    // M�todo llamado cuando el slot es clicado
    public void clickSlot()
    {
        // Lanza el evento de interacci�n con el tipo "Click" y el �ndice del slot
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);

        // Si hay un �tem seleccionado previamente para mover (arrastrar), intenta moverlo a este slot
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            // Si el �ndice de destino es diferente al de origen, se realiza la operaci�n de mover
            if (InventarioUI.Instance.IndexSlotInicialPorMover != Index)
            {
                // Mover el �tem entre los dos slots
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    }

    // M�todo para seleccionar el bot�n del slot en la UI
    public void SeleccionarSlot()
    {
        _button.Select();
    }

    // M�todo para usar el �tem en el slot
    public void SlotUsarItem()
    {
        // Verifica si hay un �tem en el slot, si es as�, lanza el evento de uso
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
        }
    }
}
