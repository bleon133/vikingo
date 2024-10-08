using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Farming/Seed")]
public class Seed : ScriptableObject
{
    public string seedName;
    public Sprite[] growthStages;
    public float timePerStage;
    public GameObject harvestPrefab; // El prefab correspondiente a este tipo de vegetal

}

