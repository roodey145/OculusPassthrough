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
        
        transform.position = Vector3.right * m_trackedPlayer.position.x + 
                             Vector3.up * (_tableSurface.tableSurfaceY + m_yOffset) + 
                             Vector3.forward * (m_trackedPlayer.position.z + m_trackedPlayerOffset);
        transform.rotation = Quaternion.Euler(90, 180 - m_trackedPlayer.rotation.y, 0);
        //transform.position = new Vector3(m_trackedPlayer.position.x, _tableSurface.tableSurfaceY + m_yOffset, m_trackedPlayer.position.z - m_trackedPlayerOffset);
    }
}
