using TMPro;
using UnityEngine;
public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);//llama la descripcion
        QuestCargado = quest;
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +// mostrar la recompensa oro
                               $"\n-{quest.RecompensaExp} exp" +// mostrar la recompensa exp
                               $"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}";
                                                                       

    } 

    public void AceptarQuest()
    {
        if (QuestCargado == null)
        {
            return;
        }

        QuestManager.Instance.AñadirQuest(QuestCargado);
        gameObject.SetActive(false);
    }
}
