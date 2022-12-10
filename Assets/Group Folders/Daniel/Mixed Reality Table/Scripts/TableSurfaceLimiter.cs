using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TableSurfaceLimiter : MonoBehaviour
{
    OVRHand m_ovrHand;
    private TableSurface _tableSurface;
    private MeshRenderer[] _modelRenderers;
    private Vector3 _centerOfMass;
    private Bounds _bounds;
    private float threshold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _tableSurface = FindObjectOfType<TableSurface>();
        _modelRenderers = GetComponentsInChildren<MeshRenderer>();
        _centerOfMass = MeshUtils.CalculateCenterOfMass(gameObject, _modelRenderers);
    }

    private void OnEnable()
    {
        _modelRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        /*
        if (IsMeshUnderSurface(out float lowestPoint))
        {
            transform.position = new Vector3(transform.position.x,
                                            _plane.position.y + (transform.TransformPoint(_mesh.bounds.center).y - lowestPoint),
                                            transform.position.z);
        }
        */
        _bounds = MeshUtils.GetMeshBoundaryAABB(gameObject, _modelRenderers);
        if (_bounds.min.y >= _tableSurface.tableSurfaceY) return;
        var position = transform.position;
        transform.position = new Vector3(position.x, _tableSurface.tableSurfaceY + (position.y - _bounds.min.y), position.z);

        // NOTES:
        // If we always want the cube to align with the surface, then don't if the cube is under the surface but simply move it.
        // We should also consider if the object should be aligned to the surface while they modify it or after they are done modifing it.


        // transform.position = Vector3.up * (plane.position.y + (transform.TransformPoint(_mesh.bounds.center).y - lowestPoint));

    }

    /// <summary>
    /// Checks if a mesh's boundary is under of the defined surface level
    /// </summary>
    /// <param name="lowestPoint">The lowest point under the surface</param>
    /// <returns>true of the mesh's boundary is under of the surface level</returns>
    private bool IsMultipleMeshesUnderSurface(out float lowestPoint)
    {
        Bounds bounds = MeshUtils.GetMeshBoundaryAABB(gameObject, _modelRenderers);
        lowestPoint = bounds.min.y;

        return lowestPoint <= _tableSurface.tableSurfaceY;
    }

    private bool IsLayingFlat()
    {
        Vector3 rotation = transform.eulerAngles;
        return rotation.x % 90 == 0
            && rotation.y % 90 == 0
            && rotation.z % 90 == 0;
    }

    private void PlaceOnFace()
    {

        Quaternion rotation = transform.rotation;
        float rotationX = rotation.eulerAngles.x - (rotation.eulerAngles.x % 90);
        float rotationY = rotation.eulerAngles.y - (rotation.eulerAngles.y % 90);
        float rotationZ = rotation.eulerAngles.z - (rotation.eulerAngles.z % 90);
        Debug.Log(new Vector3(rotationX, rotationY, rotationZ));
        transform.rotation *= Quaternion.Euler(rotationX, rotationY, rotationZ);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
#endif

}
