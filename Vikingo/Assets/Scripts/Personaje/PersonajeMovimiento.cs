using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    // Variables para controlar la velocidad del personaje y el sprint
    [SerializeField] private float velocidad; // Velocidad normal del personaje
    [SerializeField] private float velocidadSprint; // Velocidad mientras est� en sprint
    [SerializeField] private float tiempoSprint = 5f; // Duraci�n del sprint
    [SerializeField] private float tiempoRecargaSprint = 10f; // Tiempo de recarga del sprint

    private float tiempoRestanteRecarga = 0f; // Tiempo restante para recargar el sprint
    private bool enSprint = false; // Indica si el personaje est� en sprint
    private Vector2 _direccionMovimiento; // Direcci�n del movimiento del personaje

    private Rigidbody2D _rigidbody2D; // Referencia al componente Rigidbody2D para manejar la f�sica del personaje

    // Propiedad que indica si el personaje est� en movimiento
    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;
    // Propiedad que expone la direcci�n del movimiento del personaje
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private void Awake()
    {
        // Obtiene el componente Rigidbody2D del objeto al que est� asociado este script
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura el input del teclado para los ejes horizontal y vertical (WASD o flechas)
        var _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Asigna la direcci�n del movimiento en base al input del jugador
        _direccionMovimiento = new Vector2(
            _input.x != 0 ? Mathf.Sign(_input.x) : 0, // Asegura que la direcci�n sea -1, 0 o 1
            _input.y != 0 ? Mathf.Sign(_input.y) : 0
        );

        // Verifica si el jugador mantiene presionada la tecla de sprint (Shift Izquierdo) y si el sprint est� disponible
        if (Input.GetKey(KeyCode.LeftShift) && !enSprint && tiempoRestanteRecarga <= 0)
        {
            StartCoroutine(Sprint()); // Inicia la corrutina de sprint
        }

        // Normaliza la direcci�n para que su magnitud no exceda 1 (movimiento diagonal m�s suave)
        if (_direccionMovimiento.magnitude > 1)
            _direccionMovimiento.Normalize();

        // Actualiza el tiempo restante de recarga del sprint, si es necesario
        if (tiempoRestanteRecarga > 0)
        {
            tiempoRestanteRecarga -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Define la velocidad actual del personaje (normal o sprint)
        float velocidadActual = velocidad;

        if (enSprint)
        {
            velocidadActual = velocidadSprint; // Cambia a la velocidad de sprint si est� activo
        }

        // Mueve el personaje en la direcci�n indicada con la velocidad correspondiente
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidadActual * Time.fixedDeltaTime);
    }

    // Corrutina que maneja la duraci�n del sprint
    private IEnumerator Sprint()
    {
        enSprint = true; // Indica que el personaje est� en sprint
        float tiempoInicial = tiempoSprint; // Guarda el tiempo original del sprint

        // Reduce el tiempo del sprint hasta que llegue a 0
        while (tiempoSprint > 0)
        {
            tiempoSprint -= Time.deltaTime;
            yield return null;
        }

        enSprint = false; // Desactiva el sprint
        tiempoRestanteRecarga = tiempoRecargaSprint; // Inicia la recarga del sprint
        tiempoSprint = tiempoInicial; // Restaura el tiempo de sprint al valor inicial
    }
}
