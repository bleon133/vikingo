using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Este script maneja el di�logo entre el personaje y los NPCs, mostrando el di�logo en un panel en pantalla.
// Tambi�n controla la habilitaci�n/deshabilitaci�n del movimiento del personaje durante el di�logo.
public class DialogoManager : Singleton<DialogoManager>
{
    // Referencia al script que controla el movimiento del personaje.
    [SerializeField] private PersonajeMovimiento controlMovimiento;

    // Referencias a los elementos del UI que mostrar�n el di�logo.
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    // Propiedad que guarda el NPC con el que el jugador puede interactuar.
    public NPCInteraciones NPCDisponible { get; set; }

    // Cola que almacena la secuencia de di�logos que se mostrar�n.
    private Queue<string> dialogosSecuencia;

    // Variables para controlar la animaci�n del di�logo y si se ha mostrado la despedida.
    private bool dialogoAnimado;
    private bool despedidaMostrada;

    // Nueva variable para controlar si el di�logo ha sido activado
    private bool dialogoActivado = false;

    // M�todo Start se ejecuta cuando el script se inicializa. Aqu� se inicializa la cola de di�logos.
    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    // M�todo Update se ejecuta en cada frame. Aqu� se manejan las interacciones del jugador con el NPC.
    private void Update()
    {
        // Si no hay un NPC disponible para interactuar, salir del m�todo.
        if (NPCDisponible == null)
        {
            return;
        }

        // Si el jugador presiona la tecla "F" y el di�logo no ha sido activado, se configura el panel de di�logo.
        if (Input.GetKeyDown(KeyCode.F) && !dialogoActivado)
        {
            ConfigurarPanel(NPCDisponible.Dialogo);

            // Deshabilitar el movimiento del personaje mientras se muestra el di�logo.
            controlMovimiento.enabled = false;

            // Establece que el di�logo ha sido activado
            dialogoActivado = true;
        }

        // Si el jugador presiona la tecla "Space", se contin�a el di�logo o se cierra el panel.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Si la despedida ya fue mostrada, se cierra el panel de di�logo.
            if (despedidaMostrada)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrada = false;
                // Restablecer el estado de dialogoActivado si se cierra el panel
                dialogoActivado = false;
                return;
            }

            // Si hay una interacci�n extra, se abre el panel de interacci�n extra y se cierra el de di�logo.
            if (NPCDisponible.Dialogo.ContieneInteracionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
                // Restablecer el estado de dialogoActivado si se cierra el panel
                dialogoActivado = false;
                return;
            }

            // Si el di�logo se est� animando, se contin�a con el siguiente.
            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
    }

    // M�todo para abrir o cerrar el panel de di�logo. Controla tambi�n el movimiento del personaje.
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);

        // Si el panel de di�logo se cierra, se habilita nuevamente el movimiento del personaje.
        if (!estado)
        {
            controlMovimiento.enabled = true;
        }
    }

    // M�todo para configurar el panel de di�logo con la informaci�n del NPC.
    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true); // Abre el panel de di�logo.

        // Carga la secuencia de di�logos del NPC en la cola.
        CargarDialogoSencuencia(npcDialogo);

        // Configura el icono, nombre y saludo del NPC en el panel.
        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = npcDialogo.Nombre;
        MostrarTextoConAnimacion(npcDialogo.Saludo); // Muestra el saludo con animaci�n.
    }

    // M�todo que carga en la cola la secuencia de di�logos del NPC.
    private void CargarDialogoSencuencia(NPCDialogo npcDialogo)
    {
        // Si no hay di�logos en el NPC, no se hace nada.
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        // Encola cada oraci�n del di�logo en la cola de secuencia.
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    // M�todo que contin�a mostrando los di�logos en pantalla. 
    private void ContinuarDialogo()
    {
        // Si no hay NPC disponible o ya se mostr� la despedida, no hace nada.
        if (NPCDisponible == null || despedidaMostrada)
        {
            return;
        }

        // Si ya no quedan di�logos en la cola, se muestra la despedida del NPC.
        if (dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida); // Muestra la despedida.
            despedidaMostrada = true; // Marca la despedida como mostrada.
            return;
        }

        // Muestra el siguiente di�logo de la secuencia.
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    // Corrutina que anima el texto, mostrando las letras de una en una con un retraso.
    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false; // Marca el di�logo como no animado.
        npcConversacionTMP.text = ""; // Limpia el texto actual.

        // Convierte la oraci�n en un array de caracteres.
        char[] letras = oracion.ToCharArray();

        // Recorre cada letra, a�adi�ndola al texto con un retraso para la animaci�n.
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.01f); // Controla la velocidad de la animaci�n.
        }

        dialogoAnimado = true; // Marca el di�logo como animado una vez terminado.
    }

    // M�todo que inicia la corrutina para mostrar el texto con animaci�n.
    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion)); // Inicia la animaci�n del texto.
    }
}
