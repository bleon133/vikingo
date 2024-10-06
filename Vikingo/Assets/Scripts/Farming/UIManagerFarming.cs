using UnityEngine;
using UnityEngine.UI;

public class UIManagerFarming : MonoBehaviour
{
    public static UIManagerFarming instance;
    public Dropdown seedDropdown;
    public Seed[] availableSeeds;  // Lista de semillas disponibles en el juego

    private void Awake()
    {
        instance = this;
        PopulateDropdown();
    }

    private void PopulateDropdown()
    {
        seedDropdown.ClearOptions();
        foreach (Seed seed in availableSeeds)
        {
            seedDropdown.options.Add(new Dropdown.OptionData(seed.seedName));
        }
    }
}
