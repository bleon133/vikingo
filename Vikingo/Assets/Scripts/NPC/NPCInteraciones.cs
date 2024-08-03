using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class NPCInteraciones : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteractuar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
            npcButtonInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
            npcButtonInteractuar.SetActive(false);
        }
    }
}
