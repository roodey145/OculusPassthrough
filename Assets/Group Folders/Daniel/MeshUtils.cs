using UnityEngine;

public static class MeshUtils
{
    /// <summary>
    /// Calculates the center of mass of the mesh
    /// </summary>
    /// <param name="parent">The parent gameobject holding all the meshrenders</param>
    /// <param name="renderers">All the meshrenderers from the mesh</param>
    /// <returns>The center of mass in world space</returns>
    public static Vector3 CalculateCenterOfMass(GameObject parent, MeshRenderer[] renderers)
    {
        var bounds = new Bounds(parent.transform.position, Vector3.zero);
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds.center;
    }
    
    /// <summary>
    /// Gets the boundary surrounding the whole mesh
    /// </summary>
    /// <param name="parent">The parent gameobject holding all the meshrenderers</param>
    /// <param name="renderers">All the meshrenderers from the mesh</param>
    /// <returns>The boundary </returns>
    public static Bounds GetMeshBoundary(GameObject parent, MeshRenderer[] renderers)
    {
        Bounds bounds = new(parent.transform.position, Vector3.zero);
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}
