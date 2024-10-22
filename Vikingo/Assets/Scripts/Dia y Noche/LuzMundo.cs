using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;

namespace TiempoMundo
{
    // Este componente requiere que el objeto tenga un componente Light2D
    [RequireComponent(typeof(Light2D))]
    public class LuzMundo : MonoBehaviour
    {
        // Variable para almacenar la referencia al componente Light2D
        private Light2D Light;

        // Referencia al sistema que controla el tiempo en el mundo
        [SerializeField]
        private TiempoMundo tiempoMundo;

        // Un gradiente que determina los colores de la luz a lo largo del d�a
        [SerializeField]
        private Gradient gradient;

        // M�todo llamado cuando el script se inicializa
        private void Awake()
        {
            // Se obtiene el componente Light2D del GameObject
            Light = GetComponent<Light2D>();

            // Se suscribe al evento que se dispara cuando el tiempo del mundo cambia
            tiempoMundo.WorldTimeChanged += OnWorldTimeChanged;
        }

        // M�todo llamado cuando el objeto es destruido
        private void OnDestroy()
        {
            // Se desuscribe del evento para evitar problemas de memoria
            tiempoMundo.WorldTimeChanged -= OnWorldTimeChanged;
        }

        // M�todo que se ejecuta cuando el evento WorldTimeChanged es disparado
        // Recibe el nuevo tiempo del mundo y actualiza el color de la luz
        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            // Cambia el color de la luz en funci�n del porcentaje del d�a transcurrido
            Light.color = gradient.Evaluate(PorcentajeDelD�a(newTime));
        }

        // M�todo que calcula el porcentaje del d�a transcurrido seg�n el tiempo actual
        private float PorcentajeDelD�a(TimeSpan timeSpan)
        {
            // Calcula el porcentaje del d�a basado en los minutos totales y el total de minutos en un d�a
            return (float)timeSpan.TotalMinutes % TiempoMundoEst�ticos.Minutoseneldia / TiempoMundoEst�ticos.Minutoseneldia;
        }
    }
}
