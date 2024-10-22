using System; // Para el uso de clases del sistema
using TMPro; // Para usar TextMeshProUGUI
using UnityEngine;
using UnityEngine.UI; // Para usar componentes UI de Unity

// Clase que representa un �tem en la tienda del juego
public class ItemTienda : MonoBehaviour
{
    [Header("Config")]
    // Referencias a los componentes UI que mostrar�n la informaci�n del �tem en la tienda
    [SerializeField] private Image itemIcono;               // �cono del �tem
    [SerializeField] private TextMeshProUGUI itemNombre;    // Nombre del �tem
    [SerializeField] private TextMeshProUGUI itemCosto;     // Costo del �tem
    [SerializeField] private TextMeshProUGUI cantidadPorComprar; // Cantidad del �tem a comprar

    // Propiedad para almacenar el �tem de venta cargado
    public ItemVenta ItemCargado { get; private set; }

    // Variables para manejar la cantidad a comprar y costos
    private int cantidad;          // Cantidad de �tems que el jugador desea comprar
    private int costoInicial;      // Costo inicial del �tem
    private int costoActual;       // Costo actual basado en la cantidad seleccionada

    // M�todo Update, se llama en cada frame
    private void Update()
    {
        // Actualiza la UI con la cantidad y el costo actual del �tem
        cantidadPorComprar.text = cantidad.ToString(); // Muestra la cantidad a comprar
        itemCosto.text = costoActual.ToString();       // Muestra el costo actual
    }

    // M�todo para configurar el �tem de venta con la informaci�n del ItemVenta
    public void ConfigurarItemVenta(ItemVenta itemVenta)
    {
        // Almacena el �tem cargado
        ItemCargado = itemVenta;

        // Asigna el �cono, nombre y costo del �tem a los componentes de UI
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();

        // Inicializa la cantidad a 1 y establece los costos
        cantidad = 1;
        costoInicial = itemVenta.Costo;
        costoActual = itemVenta.Costo;
    }

    // M�todo para realizar la compra del �tem
    public void ComprarItem()
    {
        // Verifica si el jugador tiene suficientes monedas para realizar la compra
        if (MonedasManager.Instance.MonedasTotales >= costoActual)
        {
            // A�ade el �tem al inventario y quita las monedas gastadas
            Inventario.Instance.A�adirItem(ItemCargado.Item, cantidad);
            MonedasManager.Instance.RemoverMonedas(costoActual);

            // Reinicia la cantidad y el costo actual
            cantidad = 1;
            costoActual = costoInicial;
        }
    }

    // M�todo para sumar un �tem a la cantidad que se desea comprar
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

    // M�todo para restar un �tem de la cantidad que se desea comprar
    public void RestarItemPorComprar()
    {
        // Si la cantidad es 1, no se permite restar m�s
        if (cantidad == 1)
        {
            return; // Sale del m�todo
        }

        cantidad--; // Disminuye la cantidad
        costoActual = costoInicial * cantidad; // Actualiza el costo actual
    }
}
