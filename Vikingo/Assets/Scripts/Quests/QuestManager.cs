using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quest")]
    [SerializeField]
    private Quest[] questDisponibles;//variables

    [Header("Inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;//se carga los quest
    [SerializeField] private Transform inspectorQuestContenedor;


    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;//se carga en el panel del personaje
    [SerializeField] private Transform personajeQuestContenedor;


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


}
