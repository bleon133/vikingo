using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAccion : ScriptableObject
{
    public abstract void Ejecutar(AIController controller);
}
