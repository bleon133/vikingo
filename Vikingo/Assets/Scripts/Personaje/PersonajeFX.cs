using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFX : MonoBehaviour
{

    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad);
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDañoRecibido(float daño)
    {
        StartCoroutine(IEMostrarTexto(daño));
    }

    private void OnEnable()
    {
        AIController.EventoDañoRealizado += RespuestaDañoRecibido;
    }

    private void OnDisable()
    {
        AIController.EventoDañoRealizado -= RespuestaDañoRecibido;
    }
}
