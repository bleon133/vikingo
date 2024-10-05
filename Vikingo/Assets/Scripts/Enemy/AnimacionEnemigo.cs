using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionEnemigo : MonoBehaviour
{
    private Vector2 lastPosition; // Para almacenar la �ltima posici�n del objeto
    private Animator animator;    // Referencia al Animator

    void Start()
    {
        // Inicializamos la �ltima posici�n con la posici�n actual del objeto al empezar
        lastPosition = transform.position;

        // Obtenemos el componente Animator del objeto
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Obtenemos la posici�n actual del objeto
        Vector2 currentPosition = transform.position;

        // Calculamos la direcci�n del movimiento como la diferencia entre la posici�n actual y la �ltima posici�n
        Vector2 direction = currentPosition - lastPosition;

        // Verificamos si hay movimiento
        if (direction != Vector2.zero)
        {
            // Determinamos si el movimiento es m�s fuerte en el eje X o Y
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                // Movimiento en el eje X (izquierda o derecha)
                if (direction.x > 0)
                {
                    // Derecha
                    animator.SetFloat("X", 1);
                    animator.SetFloat("Y", 0);
                    Debug.Log("Movimiento hacia la derecha");
                }
                else
                {
                    // Izquierda
                    animator.SetFloat("X", -1);
                    animator.SetFloat("Y", 0);
                    Debug.Log("Movimiento hacia la izquierda");
                }
            }
            else
            {
                // Movimiento en el eje Y (arriba o abajo)
                if (direction.y > 0)
                {
                    // Arriba
                    animator.SetFloat("X", 0);
                    animator.SetFloat("Y", 1);
                    Debug.Log("Movimiento hacia arriba");
                }
                else
                {
                    // Abajo
                    animator.SetFloat("X", 0);
                    animator.SetFloat("Y", -1);
                    Debug.Log("Movimiento hacia abajo");
                }
            }
        }

        // Actualizamos la �ltima posici�n para la pr�xima comprobaci�n
        lastPosition = currentPosition;
    }
}
