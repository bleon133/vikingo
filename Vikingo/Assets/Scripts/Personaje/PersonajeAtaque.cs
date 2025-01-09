using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    public Arma ArmaEquipada { get; private set; }

    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        if(ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.AñadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if(ArmaEquipada == null)
        {
            return;
        }

        if(ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }
}
