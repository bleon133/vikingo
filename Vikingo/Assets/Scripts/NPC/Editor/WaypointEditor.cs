using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]

public class WaypointEditor : Editor

{
    Waypoint waypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.yellow;
        if (waypointTarget == null)
        {
            return;
        }

        for (int i = 0; i < waypointTarget.Puntos.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //crear handle

            Vector3 puntoActual = waypointTarget.posicionActual + waypointTarget.Puntos[i];

            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, Quaternion.identity, 0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle");
                waypointTarget.Puntos[i] = nuevoPunto - waypointTarget.posicionActual;
            }
        }
    }
}