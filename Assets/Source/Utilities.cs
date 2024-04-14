using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }

    public static Vector3 GetMousePositionInWorld(Camera camera)
    {
        Vector3 DesiredPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        DesiredPosition.z = 0.0f;
        return DesiredPosition;
    }
}
