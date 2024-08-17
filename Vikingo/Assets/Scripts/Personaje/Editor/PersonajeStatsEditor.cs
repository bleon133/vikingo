using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(PersonajeStats))]
public class PersonajeStatsEditor : Editor
{
    public PersonajeStats StartsTarget => target as PersonajeStats;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Resetear Valores"))
        {
            StartsTarget.ResetearValores();
        }
    }
}
