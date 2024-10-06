using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileFarming : MonoBehaviour
{
    public Seed currentSeed;  // Semilla plantada en este tile
    public int growthStage = 0;  // Etapa de crecimiento de la semilla en este tile
    public bool isReadyForHarvest = false;

    public GameObject seedSelectionUI;  // Panel de selección de semilla
    public Dropdown seedDropdown;

    public Sprite initialSprite;  // El sprite inicial cuando el tile está vacío
    private SpriteRenderer spriteRenderer;  // Referencia al SpriteRenderer del tile
    private Coroutine growthCoroutine;  // Referencia al Coroutine de crecimiento

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        seedSelectionUI.SetActive(false);  // Mantener la UI oculta hasta hacer clic
        ResetTile();  // Inicializar el tile con el sprite vacío
    }

    public void PlantSeed(Seed seed)
    {
        
        // Asignar la semilla actual y reiniciar la etapa de crecimiento
        currentSeed = seed;
        growthStage = 0;
        isReadyForHarvest = false;

        // Iniciar el proceso de crecimiento solo para este tile
        growthCoroutine = StartCoroutine(GrowSeed());
    }

    private IEnumerator GrowSeed()
    { 
        // Crecimiento de la semilla solo para este tile
        while (growthStage < currentSeed.growthStages.Length)
        {
            spriteRenderer.sprite = currentSeed.growthStages[growthStage];
            yield return new WaitForSeconds(currentSeed.timePerStage);
            growthStage++;
        }
        isReadyForHarvest = true;  // La cosecha está lista solo para este tile
    }

    public void Harvest()
    {
        if (isReadyForHarvest)
        {
            // Reiniciar el tile
            ResetTile();
        }
    }

    private void ResetTile()
    {
        currentSeed = null;
        growthStage = 0;
        isReadyForHarvest = false;
        spriteRenderer.sprite = initialSprite;  // Volver al sprite inicial (tile vacío)
    }

    private void OnMouseDown()
    {
        if (currentSeed == null)
        {
            // Mostrar el panel de selección de semilla
            PositionSelectionUI();
            seedSelectionUI.SetActive(true);
        }
        else if (isReadyForHarvest)
        {
            Harvest();  // Cosechar si está listo
        }

    }

    public void SelectSeedFromDropdown()
    {
        int seedIndex = seedDropdown.value;
        Seed selectedSeed = UIManagerFarming.instance.availableSeeds[seedIndex];
        PlantSeed(selectedSeed);
        seedSelectionUI.SetActive(false);  // Ocultar la UI después de plantar
    }

    private void PositionSelectionUI()
    {
        // Colocar el panel de selección de semilla sobre el tile
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        seedSelectionUI.transform.position = screenPos;
    }
}
