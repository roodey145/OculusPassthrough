using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private MeetingModelManager m_meetingModelManager;
    [SerializeField] private ModelManager m_modelManager;
    [SerializeField] private GameObject m_cube;
    [SerializeField] private GameObject m_sphere;
    private void OnDisable()
    {
        string modelName = "";
        if (m_meetingModelManager != null)
        {
            if (m_meetingModelManager.transform.childCount == 0)
            {
                DisableAllModels();
                return;
            }
            if(m_meetingModelManager != null) modelName = m_meetingModelManager.transform.GetChild(0).name;
        }
        
        if (m_modelManager != null)
        {
            if (m_modelManager.transform.childCount == 0)
            {
                DisableAllModels();
                return;
            }
            if(m_modelManager != null) modelName = m_modelManager.transform.GetChild(0).name;
            
        }
        
        if(modelName == "Cube") m_cube.SetActive(true);
        if(modelName == "Sphere") m_sphere.SetActive(true);
        
    }
    
    private void OnEnable()
    {
        DisableAllModels();
    }

    private void DisableAllModels()
    {
        m_cube.SetActive(false);
        m_sphere.SetActive(false);
    }
}
