using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLocationAdjuster : MonoBehaviour
{
    [SerializeField] private Transform m_hmdTracker;
    [SerializeField] private float m_distanceFromHmd;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        transform.position = m_hmdTracker.position + (Vector3.back * m_distanceFromHmd);
    }
    
}
