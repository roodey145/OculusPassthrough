using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modelCounter;
    [SerializeField] private GameObject modelsHolder;

    public GameObject[] m_models;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        modelCounter.text = $"Models Selected ({transform.childCount})";
        m_models = new GameObject[modelsHolder.transform.childCount];
        for (int i = 0; i < modelsHolder.transform.childCount; i++)
        {
            m_models[i] = modelsHolder.transform.GetChild(i).gameObject;
            int modelNumber = i;
            m_models[i].GetComponent<Button>().onClick.AddListener(() => OnModelSelected(m_models[modelNumber]));
        }

        FitModels();
    }

    private void OnModelSelected(GameObject model)
    {
        model.GetComponent<Button>().onClick.RemoveAllListeners();
        model.GetComponent<Button>().onClick.AddListener(() => OnModelDeselected(model));
        model.transform.SetParent(transform);
        UpdateModelCounter();
    }

    private void OnModelDeselected(GameObject model)
    {
        model.GetComponent<Button>().onClick.RemoveAllListeners();
        model.GetComponent<Button>().onClick.AddListener(() => OnModelSelected(model));
        model.transform.SetParent(modelsHolder.transform);
        UpdateModelCounter();
    }

    private void FitModels()
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.rect.width, 1);
    }

    private void UpdateModelCounter()
    {
        modelCounter.text = $"Models Selected ({transform.childCount})";
    }
    
}
