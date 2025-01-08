using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    public ItemArma ArmaEquipada { get; set; }

    public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma;
        armaIcono.sprite = itemArma.Arma.ArmaIcono;
        armaIcono.gameObject.SetActive(true);
        armaSkillIcono.sprite = itemArma.Arma.IconoSkill;
        armaSkillIcono.gameObject.SetActive(true);
    }

    public void RemoverArma()
    {
        Debug.Log("Remover Arma");
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        ArmaEquipada = null;
    }
}
