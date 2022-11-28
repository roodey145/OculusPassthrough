using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetingModelManager : MonoBehaviour
{
    [SerializeField] private Transform m_modelHolder;

    [SerializeField] private GameObject m_missingModelMessage;
    public GameObject[] m_models;
    private GameObject _visibleMissingModelMessage;

    private void Start()
    {
        m_models = new GameObject[m_modelHolder.childCount];
        for (int i = 0; i < m_modelHolder.childCount; i++)
        {
            m_models[i] = m_modelHolder.GetChild(i).gameObject;
            int modelNumber = i;
            m_models[i].GetComponent<Button>().onClick.AddListener(() => OnModelSelected(m_models[modelNumber]));
        }
    }

    private void OnModelSelected(GameObject model)
    {
        model.transform.SetParent(transform);
        model.GetComponent<Button>().onClick.RemoveAllListeners();
        model.GetComponent<Button>().onClick.AddListener(() => OnModelDeselected(model));
    }

    private void OnModelDeselected(GameObject model)
    {
        model.transform.SetParent(m_modelHolder);
        model.GetComponent<Button>().onClick.RemoveAllListeners();
        model.GetComponent<Button>().onClick.AddListener(() => OnModelSelected(model));
        
    }

    private bool NoModelsSelected()
    {
        return transform.childCount == 0;
    }

    private void ShowNoModelsMessage()
    {
        _visibleMissingModelMessage = Instantiate(m_missingModelMessage, transform.position, Quaternion.identity, transform);
        _visibleMissingModelMessage.GetComponent<RectTransform>().position = Vector3.zero;
    }

    private void HideNoModelMessage()
    {
        if (transform.GetChild(0).name != "Container") return;
        Destroy(transform.GetChild(0).gameObject);
    }
    
    
}
