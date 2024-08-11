using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionPorSegundo;

    public float ManaActual { get; private set; }

    private void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G)) 
        {
            UsarMana(10f);
        }
    }

    public void UsarMana(float cantidad)
    {
        if(ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}
