using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldData : MonoBehaviour
{
    public TextMeshProUGUI m_errorMessage;
    public InputFieldType m_inputFieldType;
    public TMP_InputField m_inputField;
    public Color m_inputFieldNormalColor;

    private void Awake()
    {
        m_inputField = GetComponentInChildren<TMP_InputField>();
        m_inputFieldNormalColor = m_inputField.image.color;
    }
    
    
    
}
