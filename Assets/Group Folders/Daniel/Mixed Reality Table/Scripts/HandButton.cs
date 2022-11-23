using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class HandButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UIMenu;
    private RoundedBoxProperties _roundedBoxProperties;
    private bool _isSelected;
    private float _selectedInnerRadius = 0.5f;

    private void Awake()
    {
        _roundedBoxProperties = transform.GetComponentInChildren<RoundedBoxProperties>();
        m_UIMenu.SetActive(true);
    }

    public void ToggleSelectedText()
    {
        _isSelected = !_isSelected;
        _roundedBoxProperties.BorderInnerRadius = (_isSelected) ? _selectedInnerRadius : 0;
        m_UIMenu.SetActive(_isSelected);
    }
}
