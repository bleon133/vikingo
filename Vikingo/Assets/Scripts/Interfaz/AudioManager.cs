using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource generalAudioSource;
    public AudioSource ambientAudioSource;
    public AudioSource effectsAudioSource;

    public Slider generalSlider;
    public Slider ambientSlider;
    public Slider effectsSlider;

    private void Start()
    {
        // Inicializar los sliders con el volumen actual
        generalSlider.value = generalAudioSource.volume;
        ambientSlider.value = ambientAudioSource.volume;
        effectsSlider.value = effectsAudioSource.volume;

        // Asociar eventos a los sliders
        generalSlider.onValueChanged.AddListener(UpdateGeneralVolume);
        ambientSlider.onValueChanged.AddListener(UpdateAmbientVolume);
        effectsSlider.onValueChanged.AddListener(UpdateEffectsVolume);
    }

    // Métodos del AudioManager
    private void UpdateGeneralVolume(float volume)
    {
        generalAudioSource.volume = volume;
    }

    private void UpdateAmbientVolume(float volume)
    {
        ambientAudioSource.volume = volume;
    }

    private void UpdateEffectsVolume(float volume)
    {
        effectsAudioSource.volume = volume;
    }
}
