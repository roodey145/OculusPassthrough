using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTableSurface : MonoBehaviour
{
    private TableSurface _tableSurface;
    [SerializeField] private float m_trackedPlayerOffset;
    [SerializeField] private Transform m_trackedPlayer;
    [SerializeField] private float m_yOffset;

    private void Start()
    {
        _tableSurface = FindObjectOfType<TableSurface>();
    }
    void Update()
    {
        if (_tableSurface.surfaceLevelSet) return;
        
        transform.position = new Vector3(m_trackedPlayer.position.x, _tableSurface.tableSurfaceY + m_yOffset, m_trackedPlayer.position.z - m_trackedPlayerOffset);
    }
}
