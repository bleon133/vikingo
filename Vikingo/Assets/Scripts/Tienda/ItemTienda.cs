using System; // Para el uso de clases del sistema
using TMPro; // Para usar TextMeshProUGUI
using UnityEngine;
using UnityEngine.UI; // Para usar componentes UI de Unity

// Clase que representa un ítem en la tienda del juego
public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    // Referencias a los componentes UI que mostrarán la información del ítem en la tienda
    [SerializeField] private Image itemIcono;               // Ícono del ítem
    [SerializeField] private TextMeshProUGUI itemNombre;    // Nombre del ítem
    [SerializeField] private TextMeshProUGUI itemCosto;     // Costo del ítem
    [SerializeField] private TextMeshProUGUI cantidadPorComprar; // Cantidad del ítem a comprar

    // Propiedad para almacenar el ítem de venta cargado
    public ItemVenta ItemCargado { get; private set; }

    // Variables para manejar la cantidad a comprar y costos
    private int cantidad;          // Cantidad de ítems que el jugador desea comprar
    private int costoInicial;      // Costo inicial del ítem
    private int costoActual;       // Costo actual basado en la cantidad seleccionada

    // Método Update, se llama en cada frame
    private void Update()
    {
        // Actualiza la UI con la cantidad y el costo actual del ítem
        cantidadPorComprar.text = cantidad.ToString(); // Muestra la cantidad a comprar
        itemCosto.text = costoActual.ToString();       // Muestra el costo actual
    }

    // Método para configurar el ítem de venta con la información del ItemVenta
    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        // Almacena el ítem cargado
        ItemCargado = itemVenta;

        // Asigna el ícono, nombre y costo del ítem a los componentes de UI
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();

        // Inicializa la cantidad a 1 y establece los costos
        cantidad = 1;
        costoInicial = itemVenta.Costo;
        costoActual = itemVenta.Costo;
    }

    // Método para realizar la compra del ítem
    public void ComprarItem()
    {
        // Verifica si el jugador tiene suficientes monedas para realizar la compra
        if (MonedasManager.Instance.MonedasTotales >= costoActual)
        {
            // Añade el ítem al inventario y quita las monedas gastadas
            Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
            MonedasManager.Instance.RemoverMonedas(costoActual);

            // Reinicia la cantidad y el costo actual
            cantidad = 1;
            costoActual = costoInicial;
        }
    }

    // Método para sumar un ítem a la cantidad que se desea comprar
    public void SumarItemPorComprar()
    {
        // Calcula el costo total si se aumenta la cantidad
        int costoDeCompra = costoInicial * (cantidad + 1);

        // Verifica si el jugador tiene suficientes monedas para comprar la nueva cantidad
        if (MonedasManager.Instance.MonedasTotales >= costoDeCompra)
        {
            cantidad++; // Aumenta la cantidad
            costoActual = costoInicial * cantidad; // Actualiza el costo actual
        }
    }

    // Método para restar un ítem de la cantidad que se desea comprar
    public void RestarItemPorComprar()
    {
        // Si la cantidad es 1, no se permite restar más
        if (cantidad == 1)
        {
            return; // Sale del método
        }

        cantidad--; // Disminuye la cantidad
        costoActual = costoInicial * cantidad; // Actualiza el costo actual
    }
}
