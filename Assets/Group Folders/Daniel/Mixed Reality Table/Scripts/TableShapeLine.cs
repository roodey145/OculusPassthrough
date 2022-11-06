using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TableShapeLine : MonoBehaviour
{
    [SerializeField] private OVRHand m_ovrHand;
    [SerializeField] private Transform m_finger;
    private LineRenderer _lineRenderer;
    private Vector3[] _tableVertices = new Vector3[2];
    public bool _firstCornerSet;
    public bool _secondCornerSet;

    private bool _isPinching;
    private float _prevWidth;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPositions(_tableVertices);
        _isPinching = true;
    }
    
    void Update()
    {
        //_isPinching = m_ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && m_ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);
        
        if (_isPinching)
        {
            var fingerPos = m_finger.position;
            if(!_firstCornerSet) _tableVertices[0] = fingerPos;
            if(!_secondCornerSet) _tableVertices[1] = fingerPos;
        }
        
        _lineRenderer.startWidth = Mathf.Abs(m_finger.position.z);
        for (int i = 0; i < _tableVertices.Length; i++)
        {
            _tableVertices[i] = new Vector3(_tableVertices[i].x, _tableVertices[i].y, _tableVertices[i].z + (_prevWidth - _lineRenderer.startWidth) / 2);
        }

        _prevWidth = _lineRenderer.startWidth;
        _lineRenderer.SetPositions(_tableVertices);
    }
}
