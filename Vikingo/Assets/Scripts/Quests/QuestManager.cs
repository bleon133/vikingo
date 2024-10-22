using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje; // Referencia al personaje para otorgar experiencia e ítems.

    [Header("Quest")]
    [SerializeField]
    private Quest[] questDisponibles; // Arreglo de quests disponibles.

    [Header("Inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab; // Prefab para cargar las descripciones de quests en el inspector.
    [SerializeField] private Transform inspectorQuestContenedor; // Contenedor para las descripciones de quests en el panel inspector.

    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab; // Prefab para cargar las descripciones de quests en el panel del personaje.
    [SerializeField] private Transform personajeQuestContenedor; // Contenedor para las descripciones de quests en el panel del personaje.

    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado; // Panel que muestra información cuando se completa una quest.
    [SerializeField] private TextMeshProUGUI questNombre; // Texto para mostrar el nombre de la quest completada.
    [SerializeField] private TextMeshProUGUI questRecompensaOro; // Texto para mostrar la cantidad de oro como recompensa de la quest.
    [SerializeField] private TextMeshProUGUI questRecompensaExp; // Texto para mostrar la cantidad de experiencia como recompensa de la quest.
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad; // Texto para mostrar la cantidad de ítems como recompensa de la quest.
    [SerializeField] private Image questRecompensaItemIcono; // Imagen que muestra el ícono del ítem otorgado como recompensa.

    public Quest QuestPorReclamar { get; private set; } // Propiedad para almacenar la quest por reclamar la recompensa.

    private void Start()
    {
        // Al iniciar, carga las quests en el panel del inspector.
        CargarQuestEnInspector();
    }

    private void Update()
    {
        // Prueba rápida para añadir progreso a varias quests presionando la tecla "V".
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestEnInspector() // Método para cargar las quests en el panel del inspector.
    {
        // Recorre todas las quests disponibles.
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            // Instancia un nuevo prefab de quest en el contenedor correspondiente.
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            // Configura la interfaz de usuario para la quest actual.
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AñadirQuestPorCompletar(Quest questPorCompletar) // Añade una quest al panel de quests del personaje.
    {
        // Instancia un nuevo prefab de quest en el contenedor del personaje.
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        // Configura la interfaz de usuario para la quest completada.
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AñadirQuest(Quest questPorCompletar) // Método público para añadir quests.
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa() // Método para reclamar la recompensa de una quest.
    {
        if (QuestPorReclamar == null)
        {
            return; // Si no hay ninguna quest por reclamar, no se realiza ninguna acción.
        }

        // Añade la recompensa de monedas.
        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);

        // Añade la experiencia obtenida al personaje.
        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);

        // Añade el ítem de la recompensa al inventario del personaje.
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);

        // Desactiva el panel de la quest completada.
        panelQuestCompletado.SetActive(false);

        // Elimina la referencia a la quest por reclamar, ya que se ha reclamado.
        QuestPorReclamar = null;
    }

    public void AñadirProgreso(string questID, int cantidad) // Método para añadir progreso a una quest específica.
    {
        // Busca la quest correspondiente mediante su ID.
        Quest questPorActualizar = QuestExiste(questID);
        // Añade la cantidad de progreso.
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private Quest QuestExiste(string questID) // Método para comprobar si una quest existe mediante su ID.
    {
        // Recorre las quests disponibles.
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            // Retorna la quest si coincide con el ID.
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }
        return null; // Si no existe, retorna null.
    }

    private void MostrarQuestCompletado(Quest questCompletado) // Método para mostrar la información de una quest completada.
    {
        panelQuestCompletado.SetActive(true); // Activa el panel de quest completada.
        questNombre.text = questCompletado.Nombre; // Actualiza el nombre de la quest completada.
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString(); // Muestra la cantidad de oro.
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString(); // Muestra la cantidad de experiencia.
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString(); // Muestra la cantidad de ítems.
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono; // Muestra el ícono del ítem de recompensa.
    }

    private void QuestCompletadoRespuesta(Quest questCompletado) // Método para gestionar la respuesta de completar una quest.
    {
        // Busca la quest completada y la asigna para reclamar la recompensa.
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar); // Muestra el panel de quest completada si existe.
        }
    }

    private void OnEnable() // Suscribe el método al evento de quest completada cuando el script está habilitado.
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable() // Desuscribe el método al evento de quest completada cuando el script está deshabilitado.
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
