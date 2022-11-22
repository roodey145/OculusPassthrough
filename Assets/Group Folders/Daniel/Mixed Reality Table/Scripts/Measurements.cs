using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Measurements : MonoBehaviour
{
    [SerializeField]
    private GameObject m_textPrefab;
    private BoxCollider _boxCollider;
    private GameObject[] _edgeMeasurements = new GameObject[24];

    void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        for (int i = 0; i < _edgeMeasurements.Length; i++)
        {
            _edgeMeasurements[i] = Instantiate(m_textPrefab, Vector3.zero, Quaternion.identity, transform);
            _edgeMeasurements[i].SetActive(false);
        }
    }

    void Update()
    {

    }
}
