using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TableSurfaceLimiter : MonoBehaviour
{
    [SerializeField]
    private Transform _plane;
    [SerializeField]
    private TableSurface _tableSurface;
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private GameObject _model;
    private MeshRenderer[] _modelRenderers;
    private Mesh _mesh;
    private Mesh[] _meshes;
    private Vector3 centerOfMass;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = _meshFilter.mesh;
        _meshes = _model.GetComponentsInChildren<MeshFilter>().Select(filter => filter.mesh).ToArray();
        _modelRenderers = _model.GetComponentsInChildren<MeshRenderer>();
        centerOfMass = MeshUtils.CalculateCenterOfMass(_model, _modelRenderers);
        GameObject center = Instantiate(new GameObject("CenterOfMass"), centerOfMass, Quaternion.identity, _model.transform);
        _model.transform.RotateAround(center.transform.position, Vector3.up, 90f);

    }

    // TODO - Only run when the model is modified to save ressources
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

        Bounds bounds = MeshUtils.GetComplexMeshBoundary(_model, _modelRenderers);
        _model.transform.position = new Vector3(_model.transform.position.x,
                                        _plane.position.y + (_model.transform.TransformPoint(_mesh.bounds.center).y - bounds.min.y),
                                        _model.transform.position.z);

        // NOTES:
        // If we always want the cube to align with the surface, then don't if the cube is under the surface but simply move it.
        // We should also consider if the object should be aligned to the surface while they modify it or after they are done modifing it.


        // transform.position = Vector3.up * (plane.position.y + (transform.TransformPoint(_mesh.bounds.center).y - lowestPoint));

    }

    // TODO - Optimize this for more complex meshes since this maybe scales horribly and also submeshes
    private bool IsMeshUnderSurface(out float lowestPoint)
    {
        lowestPoint = _mesh.vertices.Min(v => transform.TransformPoint(v).y);
        return lowestPoint <= _plane.position.y;
    }
    private bool IsMultipleMeshesUnderSurface(out float lowestPoint)
    {
        Bounds bounds = MeshUtils.GetComplexMeshBoundary(_model, _modelRenderers);
        lowestPoint = bounds.min.y;
        /*

        foreach (Mesh mesh in _meshes)
        {
            float lowestPointTemp = _meshes.SelectMany(v => v.vertices)
                                    .Min(v => transform.TransformPoint(v).y);
            if (lowestPointTemp < lowestPoint) lowestPointTemp = lowestPoint;
        }
        */

        return lowestPoint <= _plane.position.y;
    }
}
