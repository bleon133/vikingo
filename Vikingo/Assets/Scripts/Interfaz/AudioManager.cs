using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Fuentes de Audio")]
    public AudioSource fuenteEfectos;   // Fuente de sonido para efectos
    public AudioSource fuenteAmbiental;   // Fuente de sonido para ambiental
    public AudioSource fuenteMusica;      // Fuente de sonido para m�sica

    [Header("Deslizadores")]
    public Slider deslizadorEfectos;      // Deslizador para efectos
    public Slider deslizadorAmbiental;     // Deslizador para ambiental
    public Slider deslizadorMusica;        // Deslizador para m�sica
    public Slider deslizadorMaestro;       // Deslizador para el volumen general

    private void Start()
    {
        // Inicializa los deslizadores con los vol�menes actuales
        deslizadorEfectos.value = fuenteEfectos.volume;
        deslizadorAmbiental.value = fuenteAmbiental.volume;
        deslizadorMusica.value = fuenteMusica.volume;
        UpdateVolumenMaestro();

        // A�adir listeners a los deslizadores
        deslizadorEfectos.onValueChanged.AddListener(AjustarVolumenEfectos);
        deslizadorAmbiental.onValueChanged.AddListener(AjustarVolumenAmbiental);
        deslizadorMusica.onValueChanged.AddListener(AjustarVolumenMusica);
        deslizadorMaestro.onValueChanged.AddListener(AjustarVolumenMaestro);
    }

    // M�todos para ajustar vol�menes
    public void AjustarVolumenEfectos(float volumen)
    {
        fuenteEfectos.volume = volumen;
        // No actualiza el maestro aqu�
    }

    public void AjustarVolumenAmbiental(float volumen)
    {
        fuenteAmbiental.volume = volumen;
        // No actualiza el maestro aqu�
    }

    public void AjustarVolumenMusica(float volumen)
    {
        fuenteMusica.volume = volumen;
        // No actualiza el maestro aqu�
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
        // Actualiza el deslizador maestro basado en los vol�menes de los efectos, ambiental y m�sica
        float volumenPromedio = (fuenteEfectos.volume + fuenteAmbiental.volume + fuenteMusica.volume) / 3;
        deslizadorMaestro.value = volumenPromedio;
    }
}
