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

        // Un gradiente que determina los colores de la luz a lo largo del día
        [SerializeField]
        private Gradient gradient;

        // Método llamado cuando el script se inicializa
        private void Awake()
        {
            // Se obtiene el componente Light2D del GameObject
            Light = GetComponent<Light2D>();

            // Se suscribe al evento que se dispara cuando el tiempo del mundo cambia
            tiempoMundo.WorldTimeChanged += OnWorldTimeChanged;
        }

        // Método llamado cuando el objeto es destruido
        private void OnDestroy()
        {
            // Se desuscribe del evento para evitar problemas de memoria
            tiempoMundo.WorldTimeChanged -= OnWorldTimeChanged;
        }

        // Método que se ejecuta cuando el evento WorldTimeChanged es disparado
        // Recibe el nuevo tiempo del mundo y actualiza el color de la luz
        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            // Cambia el color de la luz en función del porcentaje del día transcurrido
            Light.color = gradient.Evaluate(PorcentajeDelDía(newTime));
        }

        // Método que calcula el porcentaje del día transcurrido según el tiempo actual
        private float PorcentajeDelDía(TimeSpan timeSpan)
        {
            // Calcula el porcentaje del día basado en los minutos totales y el total de minutos en un día
            return (float)timeSpan.TotalMinutes % TiempoMundoEstáticos.Minutoseneldia / TiempoMundoEstáticos.Minutoseneldia;
        }
    }
}
