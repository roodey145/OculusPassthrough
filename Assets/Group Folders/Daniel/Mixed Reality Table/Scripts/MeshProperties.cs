using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshProperties : MonoBehaviour
{

    public Vector3 CenterOfMass
    {
        private set;
        get;
    }
    public Bounds BoundsAABB
    {
        private set;
        get;
    }
    public Bounds BoundsOBB
    {
        private set;
        get;
    }

    public GameObject Collider
    {
        private set;
        get;
    }

    public BoxCollider BoxCollider
    {
        private set;
        get;
    }

    private MeshRenderer[] meshRenderers;

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        CenterOfMass = CalculateCenterOfMass();
        BoundsAABB = GetMeshBoundaryAABB();
        Collider = Instantiate(new GameObject("OBB_Collider"), transform.position, Quaternion.identity, transform);
        BoxCollider = Collider.AddComponent<BoxCollider>();
        BoxCollider.center = transform.InverseTransformPoint(BoundsAABB.center);
        BoxCollider.size = BoundsAABB.size;
    }

    /// <summary>
    /// Calculates the center of mass of the mesh
    /// </summary>
    /// <param name="parent">The parent gameobject holding all the meshrenders</param>
    /// <param name="renderers">All the meshrenderers from the mesh</param>
    /// <returns>The center of mass in world space</returns>
    private Vector3 CalculateCenterOfMass()
    {
        var bounds = new Bounds(transform.position, Vector3.zero);
        foreach (var renderer in meshRenderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds.center;
    }

    /// <summary>
    /// Gets the boundary surrounding the whole mesh with a AABB axis aligned Bounding box
    /// </summary>
    /// <param name="parent">The parent gameobject holding all the meshrenderers</param>
    /// <param name="renderers">All the meshrenderers from the mesh</param>
    /// <returns>The boundary </returns>
    private Bounds GetMeshBoundaryAABB()
    {
        Bounds bounds = new(transform.position, Vector3.zero);
        foreach (var renderer in meshRenderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Bounds bounds = GetMeshBoundaryAABB();
        //Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
#endif
}