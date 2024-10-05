using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar LINQ (FirstOrDefault)
using UnityEngine;
using UnityEngine.Events;

namespace TiempoMundo
{
    public class ObjetosInteractivos : MonoBehaviour
    {
        [SerializeField]
        private TiempoMundo tiempoMundo;

        [SerializeField]
        private List<Schedule> schedule;

        private void Start()
        {
            if (tiempoMundo != null)
            {
                tiempoMundo.WorldTimeChanged += CheckSchedule;
            }
        }

        private void OnDestroy()
        {
            if (tiempoMundo != null)
            {
                tiempoMundo.WorldTimeChanged -= CheckSchedule;
            }
        }

        private void CheckSchedule(object sender, TimeSpan newTime)
        {
            var currentSchedule =
                schedule.FirstOrDefault(s =>
                s.Hora == newTime.Hours &&
                s.Minuto == newTime.Minutes);

            currentSchedule?.Acción?.Invoke();
        }

        [Serializable]
        private class Schedule
        {
            public int Hora;
            public int Minuto;
            public UnityEvent Acción;
        }
    }
}