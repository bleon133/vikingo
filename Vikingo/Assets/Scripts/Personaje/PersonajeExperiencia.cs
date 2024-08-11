using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    public int Nivel { get; set; }

    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    void Start()
    {
        Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X)) 
        {
            AñadirExperiencia(2f);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        if(expObtenida > 0f)
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if(expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                ActualizarNivel();
                AñadirExperiencia(expObtenida); //Recurción
            }
            else
            {
                expActualTemp += expObtenida;
                if(expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }

        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if(Nivel < nivelMax)
        {
            Nivel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }
}
