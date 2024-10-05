using System;
using System.Collections;
using UnityEngine;

namespace TiempoMundo

{
    public class TiempoMundo : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;


        [SerializeField]
        private float duraci�nD�a; // cuanto dura el dia el segundos.

        private TimeSpan tiempoActual;
        private float duraci�nMinuto => duraci�nD�a / TiempoMundoEst�ticos.Minutoseneldia;


        private void Start()
        {
            tiempoActual = new TimeSpan(9, 0, 0);// el juego inicia a esta hora del d�a

            StartCoroutine(adicionarMinuto());// comienza ciclo de adici�n de minutos
        }
        private IEnumerator adicionarMinuto()
        {
            tiempoActual += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, tiempoActual);
            yield return new WaitForSeconds(duraci�nMinuto);
            StartCoroutine(adicionarMinuto());

        }

    }
}
