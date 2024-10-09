using UnityEngine;
using UnityEngine.UI;

public class ControladorBrillo : MonoBehaviour
{
    [SerializeField]
    private Slider brightnessSlider; // Slider para ajustar el brillo
    [SerializeField]
    private Light sceneLight; // Referencia a una fuente de luz (opcional)
    [SerializeField]
    private Camera mainCamera; // Referencia a la cámara principal (opcional)

    private void Start()
    {
        // Asegúrate de que el slider tenga el rango adecuado (0 a 1)
        brightnessSlider.minValue = 0f;
        brightnessSlider.maxValue = 1f;
        brightnessSlider.value = 1f; // Valor inicial del brillo

        // Configura el evento del slider
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
    }

    // Método para actualizar el brillo
    private void UpdateBrightness(float value)
    {
        if (sceneLight != null)
        {
            // Ajusta la intensidad de la luz
            sceneLight.intensity = value; // Cambia el brillo de la luz
        }

        if (mainCamera != null)
        {
            // Ajusta el brillo de la cámara (opcional)
            // Puedes implementar efectos de post-procesamiento si es necesario
            // Aquí se podría ajustar el color de la cámara usando una imagen en un Canvas, por ejemplo
            // mainCamera.GetComponent<Camera>().exposure = value;
        }
    }
}
