using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TableSurfaceLimiter : MonoBehaviour
{
    [SerializeField]
    private TableSurface m_tableSurface;
    private MeshRenderer[] _modelRenderers;
    private Vector3 _centerOfMass;
    private Bounds _bounds;

    // Start is called before the first frame update
    void Start()
    {
        _modelRenderers = GetComponentsInChildren<MeshRenderer>();
        _centerOfMass = MeshUtils.CalculateCenterOfMass(gameObject, _modelRenderers);
        Debug.Log(gameObject.name + ": " + _centerOfMass);
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

        _bounds = MeshUtils.GetMeshBoundary(gameObject, _modelRenderers);
        var position = transform.position;
        transform.position = new Vector3(position.x,
            m_tableSurface.tableSurfaceY + (position.y - _bounds.min.y),
                                        position.z);

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
        Bounds bounds = MeshUtils.GetMeshBoundary(gameObject, _modelRenderers);
        lowestPoint = bounds.min.y;
        /*

        foreach (Mesh mesh in _meshes)
        {
            float lowestPointTemp = _meshes.SelectMany(v => v.vertices)
                                    .Min(v => transform.TransformPoint(v).y);
            if (lowestPointTemp < lowestPoint) lowestPointTemp = lowestPoint;
        }
        */
        
        return lowestPoint <= m_tableSurface.tableSurfaceY;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
#endif
    
}
