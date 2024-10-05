
using System;
using TMPro;
using UnityEngine;

namespace TiempoMundo
{

    [RequireComponent(typeof(TMP_Text))]
    public class PantallaTiempo : MonoBehaviour
    {
        [SerializeField]

        private TiempoMundo tiempoMundo;

        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            tiempoMundo.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            tiempoMundo.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)

        {
            text.SetText(newTime.ToString(@"hh\:mm"));
        }
    }
}
