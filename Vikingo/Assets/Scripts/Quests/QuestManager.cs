using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;//para a�adir exp y item


    [Header("Quest")]
    [SerializeField]
    private Quest[] questDisponibles;//variables

    [Header("Inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;//se carga los quest
    [SerializeField] private Transform inspectorQuestContenedor;


    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;//se carga en el panel del personaje
    [SerializeField] private Transform personajeQuestContenedor;


    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado; //referencia del panel de misiones completado
    [SerializeField] private TextMeshProUGUI questNombre; //referencia del nombre de misiones completado
    [SerializeField] private TextMeshProUGUI questRecompensaOro; //recompensa del oro de misiones completado
    [SerializeField] private TextMeshProUGUI questRecompensaExp; //recompensa del exp de misiones completado
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad; //recompensa del item de misiones completado
    [SerializeField] private Image questRecompensaItemIcono; //recompensa de icono de misiones completado

    public Quest QuestPorReclamar {  get; private set; }
    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            A�adirProgreso("Mata10", 1);
            A�adirProgreso("Mata25", 1);
            A�adirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestEnInspector()//muestra en el panel de la misiones

    {
        for (int i = 0; i < questDisponibles.Length; i++)//recorre todo los quest
        {
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]); //se configura todo los quest que se ha�ade
        }
    }

    private void A�adirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void A�adirQuest(Quest questPorCompletar)
    {
        A�adirQuestPorCompletar(questPorCompletar);
    }

    public void  ReclamarRecompensa()
    {
        if (QuestPorReclamar == null)
        {
            return;
        }

        MonedasManager.Instance.A�adirMonedas(QuestPorReclamar.RecompensaOro);//a�adir monedas.

        personaje.PersonajeExperiencia.A�adirExperiencia(QuestPorReclamar.RecompensaExp); //a�adir experiencia

        Inventario.Instance.A�adirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);// a�adir item y la cantidad

        panelQuestCompletado.SetActive(false);// desativar el panel completo

        QuestPorReclamar = null; // ya se reclamo

    }



    public void A�adirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.A�adirProgreso(cantidad);
    }

    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            if (questDisponibles[i].ID == questID)
            {
                return questDisponibles[i];
            }
        }
        return null;
    }

    private void MostrarQuestCompletado(Quest questCcompletado)//metodo de actualizar los componetes panel completado
    {
        panelQuestCompletado.SetActive(true);
        questNombre.text = questCcompletado.Nombre;
        questRecompensaOro.text = questCcompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCcompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCcompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCcompletado.RecompensaItem.Item.Icono;

    }

    private void QuestCompletadoRespuesta(Quest questCompletado) //llamar el metodo de questcompletado si existe
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }

    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta; //muestra el panel de mision completado

    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
