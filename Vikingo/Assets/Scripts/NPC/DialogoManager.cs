using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Este script maneja el diálogo entre el personaje y los NPCs, mostrando el diálogo en un panel en pantalla.
// También controla la habilitación/deshabilitación del movimiento del personaje durante el diálogo.
public class DialogoManager : Singleton<DialogoManager>
{
    // Referencia al script que controla el movimiento del personaje.
    [SerializeField] private PersonajeMovimiento controlMovimiento;

    // Referencias a los elementos del UI que mostrarán el diálogo.
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    // Propiedad que guarda el NPC con el que el jugador puede interactuar.
    public NPCInteraciones NPCDisponible { get; set; }

    // Cola que almacena la secuencia de diálogos que se mostrarán.
    private Queue<string> dialogosSecuencia;

    // Variables para controlar la animación del diálogo y si se ha mostrado la despedida.
    private bool dialogoAnimado;
    private bool despedidaMostrada;

    // Nueva variable para controlar si el diálogo ha sido activado
    private bool dialogoActivado = false;

    // Método Start se ejecuta cuando el script se inicializa. Aquí se inicializa la cola de diálogos.
    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    // Método Update se ejecuta en cada frame. Aquí se manejan las interacciones del jugador con el NPC.
    private void Update()
    {
        // Si no hay un NPC disponible para interactuar, salir del método.
        if (NPCDisponible == null)
        {
            return;
        }

        // Si el jugador presiona la tecla "F" y el diálogo no ha sido activado, se configura el panel de diálogo.
        if (Input.GetKeyDown(KeyCode.F) && !dialogoActivado)
        {
            ConfigurarPanel(NPCDisponible.Dialogo);

            // Deshabilitar el movimiento del personaje mientras se muestra el diálogo.
            controlMovimiento.enabled = false;

            // Establece que el diálogo ha sido activado
            dialogoActivado = true;
        }

        // Si el jugador presiona la tecla "Space", se continúa el diálogo o se cierra el panel.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Si la despedida ya fue mostrada, se cierra el panel de diálogo.
            if (despedidaMostrada)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrada = false;
                // Restablecer el estado de dialogoActivado si se cierra el panel
                dialogoActivado = false;
                return;
            }

            // Si hay una interacción extra, se abre el panel de interacción extra y se cierra el de diálogo.
            if (NPCDisponible.Dialogo.ContieneInteracionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
                // Restablecer el estado de dialogoActivado si se cierra el panel
                dialogoActivado = false;
                return;
            }

            // Si el diálogo se está animando, se continúa con el siguiente.
            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
    }

    // Método para abrir o cerrar el panel de diálogo. Controla también el movimiento del personaje.
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);

        // Si el panel de diálogo se cierra, se habilita nuevamente el movimiento del personaje.
        if (!estado)
        {
            controlMovimiento.enabled = true;
        }
    }

    // Método para configurar el panel de diálogo con la información del NPC.
    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true); // Abre el panel de diálogo.

        // Carga la secuencia de diálogos del NPC en la cola.
        CargarDialogoSencuencia(npcDialogo);

        // Configura el icono, nombre y saludo del NPC en el panel.
        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = npcDialogo.Nombre;
        MostrarTextoConAnimacion(npcDialogo.Saludo); // Muestra el saludo con animación.
    }

    // Método que carga en la cola la secuencia de diálogos del NPC.
    private void CargarDialogoSencuencia(NPCDialogo npcDialogo)
    {
        // Si no hay diálogos en el NPC, no se hace nada.
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        // Encola cada oración del diálogo en la cola de secuencia.
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
    }

    // Método que continúa mostrando los diálogos en pantalla. 
    private void ContinuarDialogo()
    {
        // Si no hay NPC disponible o ya se mostró la despedida, no hace nada.
        if (NPCDisponible == null || despedidaMostrada)
        {
            return;
        }

        // Si ya no quedan diálogos en la cola, se muestra la despedida del NPC.
        if (dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida); // Muestra la despedida.
            despedidaMostrada = true; // Marca la despedida como mostrada.
            return;
        }

        // Muestra el siguiente diálogo de la secuencia.
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }

    // Corrutina que anima el texto, mostrando las letras de una en una con un retraso.
    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false; // Marca el diálogo como no animado.
        npcConversacionTMP.text = ""; // Limpia el texto actual.

        // Convierte la oración en un array de caracteres.
        char[] letras = oracion.ToCharArray();

        // Recorre cada letra, añadiéndola al texto con un retraso para la animación.
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.01f); // Controla la velocidad de la animación.
        }

        dialogoAnimado = true; // Marca el diálogo como animado una vez terminado.
    }

    // Método que inicia la corrutina para mostrar el texto con animación.
    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion)); // Inicia la animación del texto.
    }
}
