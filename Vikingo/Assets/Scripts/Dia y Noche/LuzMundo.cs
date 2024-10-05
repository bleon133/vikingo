using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;


namespace TiempoMundo
{
    [RequireComponent (typeof(Light2D))]
    public class LuzMundo : MonoBehaviour
    {
        private Light2D Light;

        [SerializeField]
        private TiempoMundo tiempoMundo;

        [SerializeField]
        private Gradient gradient;

        private void Awake()
        {
            Light = GetComponent<Light2D>();
            tiempoMundo.WorldTimeChanged += OnWorldTimeChanged;

        }
        private void OnDestroy()
        {
            tiempoMundo.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            Light.color = gradient.Evaluate(PorcentajeDelDía(newTime));
        }

        private float PorcentajeDelDía(TimeSpan timeSpan)
        {
            return (float)timeSpan.TotalMinutes % TiempoMundoEstáticos.Minutoseneldia / TiempoMundoEstáticos.Minutoseneldia;
        }


    }
}
