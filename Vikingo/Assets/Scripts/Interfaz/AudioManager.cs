using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Fuentes de Audio")]
    public AudioSource fuenteEfectos;   // Fuente de sonido para efectos
    public AudioSource fuenteAmbiental;   // Fuente de sonido para ambiental
    public AudioSource fuenteMusica;      // Fuente de sonido para música

    [Header("Deslizadores")]
    public Slider deslizadorEfectos;      // Deslizador para efectos
    public Slider deslizadorAmbiental;     // Deslizador para ambiental
    public Slider deslizadorMusica;        // Deslizador para música
    public Slider deslizadorMaestro;       // Deslizador para el volumen general

    private void Start()
    {
        // Inicializa los deslizadores con los volúmenes actuales
        deslizadorEfectos.value = fuenteEfectos.volume;
        deslizadorAmbiental.value = fuenteAmbiental.volume;
        deslizadorMusica.value = fuenteMusica.volume;
        UpdateVolumenMaestro();

        // Añadir listeners a los deslizadores
        deslizadorEfectos.onValueChanged.AddListener(AjustarVolumenEfectos);
        deslizadorAmbiental.onValueChanged.AddListener(AjustarVolumenAmbiental);
        deslizadorMusica.onValueChanged.AddListener(AjustarVolumenMusica);
        deslizadorMaestro.onValueChanged.AddListener(AjustarVolumenMaestro);
    }

    // Métodos para ajustar volúmenes
    public void AjustarVolumenEfectos(float volumen)
    {
        fuenteEfectos.volume = volumen;
        // No actualiza el maestro aquí
    }

    public void AjustarVolumenAmbiental(float volumen)
    {
        fuenteAmbiental.volume = volumen;
        // No actualiza el maestro aquí
    }

    public void AjustarVolumenMusica(float volumen)
    {
        fuenteMusica.volume = volumen;
        // No actualiza el maestro aquí
    }

    public void AjustarVolumenMaestro(float volumen)
    {
        // Ajusta el volumen de cada fuente de audio basado en el volumen maestro
        fuenteEfectos.volume = volumen;
        fuenteAmbiental.volume = volumen;
        fuenteMusica.volume = volumen;

        // Actualiza los deslizadores individuales para reflejar el cambio
        deslizadorEfectos.value = fuenteEfectos.volume;
        deslizadorAmbiental.value = fuenteAmbiental.volume;
        deslizadorMusica.value = fuenteMusica.volume;
    }

    private void UpdateVolumenMaestro()
    {
        // Actualiza el deslizador maestro basado en los volúmenes de los efectos, ambiental y música
        float volumenPromedio = (fuenteEfectos.volume + fuenteAmbiental.volume + fuenteMusica.volume) / 3;
        deslizadorMaestro.value = volumenPromedio;
    }
}
