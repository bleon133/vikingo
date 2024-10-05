using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionEnemigo : MonoBehaviour
{
    private Vector2 lastPosition; // Para almacenar la última posición del objeto
    private Animator animator;    // Referencia al Animator

    void Start()
    {
        // Inicializamos la última posición con la posición actual del objeto al empezar
        lastPosition = transform.position;

        // Obtenemos el componente Animator del objeto
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Obtenemos la posición actual del objeto
        Vector2 currentPosition = transform.position;

        // Calculamos la dirección del movimiento como la diferencia entre la posición actual y la última posición
        Vector2 direction = currentPosition - lastPosition;

        // Verificamos si hay movimiento
        if (direction != Vector2.zero)
        {
            // Determinamos si el movimiento es más fuerte en el eje X o Y
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

        // Actualizamos la última posición para la próxima comprobación
        lastPosition = currentPosition;
    }
}
