using System;
using System.Collections;
using UnityEngine;

namespace TiempoMundo

{
    public class TiempoMundo : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;


        [SerializeField]
        private float duraciónDía; // cuanto dura el dia el segundos.

        private TimeSpan tiempoActual;
        private float duraciónMinuto => duraciónDía / TiempoMundoEstáticos.Minutoseneldia;


        private void Start()
        {
            tiempoActual = new TimeSpan(9, 0, 0);// el juego inicia a esta hora del día

            StartCoroutine(adicionarMinuto());// comienza ciclo de adición de minutos
        }
        private IEnumerator adicionarMinuto()
        {
            tiempoActual += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, tiempoActual);
            yield return new WaitForSeconds(duraciónMinuto);
            StartCoroutine(adicionarMinuto());

        }

    }
}
