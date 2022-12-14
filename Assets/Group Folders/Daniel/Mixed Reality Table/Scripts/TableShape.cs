using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Oculus.Interaction;
using UnityEngine.Assertions;

public struct MixedRealityTable
{
    public Rect Rect;
    public float YLevel;

    public MixedRealityTable(Rect rect, float yLevel)
    {
        Rect = rect;
        YLevel = yLevel;
    }
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TableShape : MonoBehaviour
{
    [SerializeField] private Transform m_finger;
    [SerializeField] private Material m_tableMaterial;
    [SerializeField]
    private OVRHand m_ovrHand;
    private bool _isPinching;
    private bool _isDefiningShape;
    private Vector3 _startCorner;
    private Vector3 _controlCorner;
    private Vector3 _TopCorner;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform controlPoint;

    private bool isStartCornerSet;
    private bool isControlCornerSet;
    private bool isTopCornerSet;

    public MixedRealityTable MixedRealityTable
    {
        private set;
        get;
    }

    private void Start()
    {

        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        Assert.IsNotNull(m_tableMaterial);
        _meshRenderer.material = m_tableMaterial;
        _isPinching = true;
    }

    private void Update()
    {
        // TODO - Make two point, start corner and control corner, to know which orientation the table is.
        //_isPinching = m_ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && m_ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);

        if (!_isPinching)
        {
            if (isStartCornerSet)
            {
                _startCorner = m_finger.position;
                return;
            }
            if (isStartCornerSet && isControlCornerSet)
            {
                _controlCorner = m_finger.position;
                return;
            }
            if (isTopCornerSet)
            {
                _TopCorner = m_finger.position;
                return;
            }
            return;
        }
        // 1. Set start corner
        //transform.position = startPoint.position;
        //float tableAngle = Mathf.Rad2Deg * Mathf.Atan2(controlPoint.position.x - startPoint.position.x, controlPoint.position.z - startPoint.position.z) - 90;
        // 2. Start control corner and adjust orientation
        //transform.rotation = Quaternion.Euler(0, tableAngle, 0);

        var fingerPosition = m_finger.position;

        //var rect = GetTableShape(startPoint.position.x, startPoint.position.z, fingerPosition.x, fingerPosition.z);
        //_meshFilter.mesh = CreatePlaneMeshFromRect(rect);

        _meshFilter.mesh = CreatePlaneMesh(startPoint.position, controlPoint.position, fingerPosition);
        //MixedRealityTable = new MixedRealityTable(rect, _startCorner.y);
    }

    private Rect GetTableShape(float xMin, float yMin, float xMax, float yMax)
    {
        return new Rect
        {
            xMin = xMin,
            yMin = yMin,
            xMax = xMax,
            yMax = yMax,
        };
    }

    private Mesh CreatePlaneMesh(Vector3 startingPoint, Vector3 controlPoint, Vector3 topCornerPoint)
    {
        var yLevel = _startCorner.y;
        var mesh = new Mesh();
        var vertices = new Vector3[4];
        vertices[0] = new Vector3(startingPoint.x, yLevel, startingPoint.z);
        vertices[1] = new Vector3(controlPoint.x, yLevel, controlPoint.z);
        vertices[2] = new Vector3(topCornerPoint.x - Vector3.Distance(vertices[0], vertices[1]), yLevel, topCornerPoint.z - controlPoint.z);
        vertices[3] = new Vector3(topCornerPoint.x, yLevel, topCornerPoint.z);

        var triangles = new[]
        {
            0, 1, 2,
            1, 3, 2
        };

        //triangles.Reverse().ToArray();

        if (topCornerPoint.x - startingPoint.x > 0 && topCornerPoint.z - startingPoint.z > 0 ||
            topCornerPoint.x - startingPoint.x < 0 && topCornerPoint.z - startingPoint.z < 0)
        {
            triangles = triangles.Reverse().ToArray();
        }


        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    private Mesh CreatePlaneMeshFromRect(Rect rect)
    {
        var yLevel = _startCorner.y;
        var mesh = new Mesh();
        var vertices = new Vector3[4];
        vertices[0] = new Vector3(rect.xMin, yLevel, rect.yMin);
        vertices[1] = new Vector3(rect.xMax, yLevel, rect.yMin);
        vertices[2] = new Vector3(rect.xMin, yLevel, rect.yMax);
        vertices[3] = new Vector3(rect.xMax, yLevel, rect.yMax);

        var triangles = new[]
        {
            0, 1, 2,
            1, 3, 2
        };

        if (rect.xMax - _startCorner.x > 0 && rect.yMax - _startCorner.z > 0 ||
            rect.xMax - _startCorner.x < 0 && rect.yMax - _startCorner.z < 0)
        {
            triangles = triangles.Reverse().ToArray();
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
