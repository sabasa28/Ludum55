using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }
}
