using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Farming/Seed")]
public class Seed : ScriptableObject
{
    public string seedName;
    public Sprite[] growthStages;
    public float timePerStage;
}

