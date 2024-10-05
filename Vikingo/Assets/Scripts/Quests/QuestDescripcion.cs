using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI questNombre;
    [SerializeField]
    private TextMeshProUGUI questDescripcion;

    public Quest QuestCargado { get; set; }


    public virtual void ConfigurarQuestUI(Quest quest)// actualizar las misiones

    {

        questNombre.text = quest.Nombre; // cargar mision
        questDescripcion.text = quest.Descripcion; // cargar mision
       
    }

}
