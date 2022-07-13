using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AuxFunctions
{
    public static bool WithinCheckDistance (float checkDist, Vector3 a, Vector3 b) 
    {
        return checkDist >= Vector3.Distance(a, b);
    }
}
