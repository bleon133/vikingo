using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    
    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        if (QuestPorCompletar.QuestCompletadoCheck)
        {
            return;
        }

        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";

    }
    public override void ConfigurarQuestUI(Quest quest)

    {
        base.ConfigurarQuestUI(quest);
        recompensaOro.text = quest.RecompensaOro.ToString();
        recompensaExp.text = quest.RecompensaExp.ToString();
        tareaObjetivo.text = $"{quest.CantidadActual}/{quest.CantidadObjetivo}";

        recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono;
        recompensaItemCantidad.text = quest.RecompensaItem.Cantidad.ToString();

    }

    private void QuestCompletadoRespuestas(Quest questCompletado)
    {
        if (questCompletado.ID == QuestPorCompletar.ID)
        {
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestPorCompletar.QuestCompletadoCheck) 
        {
            gameObject.SetActive(false); 
        }
        Quest.EventoQuestCompletado += QuestCompletadoRespuestas;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuestas;
    }


}
