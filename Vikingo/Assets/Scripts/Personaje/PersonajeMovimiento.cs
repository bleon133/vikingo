using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float velocidadSprint;
    [SerializeField] private float tiempoSprint = 5f;
    [SerializeField] private float tiempoRecargaSprint = 10f;

    private float tiempoRestanteRecarga = 0f;
    private bool enSprint = false;
    private Vector2 _direccionMovimiento;

    private Rigidbody2D _rigidbody2D;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _direccionMovimiento = new Vector2(
            _input.x != 0 ? Mathf.Sign(_input.x) : 0,
            _input.y != 0 ? Mathf.Sign(_input.y) : 0
        );

        if (Input.GetKey(KeyCode.LeftShift) && !enSprint && tiempoRestanteRecarga <= 0)
        {
            StartCoroutine(Sprint());
        }

        if (_direccionMovimiento.magnitude > 1)
            _direccionMovimiento.Normalize();

        if (tiempoRestanteRecarga > 0)
        {
            tiempoRestanteRecarga -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        float velocidadActual = velocidad;

        if (enSprint)
        {
            velocidadActual = velocidadSprint;
        }

        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidadActual * Time.fixedDeltaTime);
    }

    private IEnumerator Sprint()
    {
        enSprint = true;
        float tiempoInicial = tiempoSprint;

        while (tiempoSprint > 0)
        {
            tiempoSprint -= Time.deltaTime;
            yield return null;
        }

        enSprint = false;
        tiempoRestanteRecarga = tiempoRecargaSprint;
        tiempoSprint = tiempoInicial;
    }
}
