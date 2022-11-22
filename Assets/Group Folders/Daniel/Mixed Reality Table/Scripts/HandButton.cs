using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class HandButton : MonoBehaviour
{
    private RoundedBoxProperties _roundedBoxProperties;
    private bool _isSelected;
    private float _selectedInnerRadius = 0.5f;

    private void Awake()
    {
        _roundedBoxProperties = transform.GetComponentInChildren<RoundedBoxProperties>();
    }

    public void ToggleSelectedText()
    {
        _roundedBoxProperties.BorderInnerRadius = (_roundedBoxProperties.BorderInnerRadius == _selectedInnerRadius) ? 0 : _selectedInnerRadius;
    }
}
