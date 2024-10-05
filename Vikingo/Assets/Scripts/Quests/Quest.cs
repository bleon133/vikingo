using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletado;
    
    [Header("Info")]
    public string Nombre;
    public string ID;//saber si se completo la mision y entregar
    public int CantidadObjetivo; //cantidad para completar la mision ej 50 enemigo
   

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    public int RecompensaOro;//recompensa de dinero ganado
    public float RecompensaExp;//experiencia 
    public QuestRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual;//cantidad sumando para llegar al objectivo principal

    [HideInInspector] public bool QuestCompletadoCheck; //ya completo los objectivos

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        verificarQuestCompletado(); 
    }

    private void verificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        if (QuestCompletadoCheck)
        {
            return;
        }
        QuestCompletadoCheck = true;
        EventoQuestCompletado?.Invoke(this);
    }
}

  [Serializable]
public class QuestRecompensaItem //objectos recojido en el juego y que se pueda utilizar
{
    public InventarioItem Item;// que se entrega
    public int Cantidad; // que se le otorga
}
