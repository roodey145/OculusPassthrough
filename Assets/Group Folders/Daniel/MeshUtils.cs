using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshUtils
{
    // The position is in world space
    public static Vector3 CalculateCenterOfMass(GameObject parent, MeshRenderer[] renderers)
    {
        var bounds = new Bounds(parent.transform.position, Vector3.zero);
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds.center;
    }

    public static Bounds GetComplexMeshBoundary(GameObject parent, MeshRenderer[] renderers)
    {
        Bounds bounds = new(parent.transform.position, Vector3.zero);
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}
