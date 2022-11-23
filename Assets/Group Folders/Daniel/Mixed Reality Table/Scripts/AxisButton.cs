using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class AxisButton : MonoBehaviour
{
    private RoundedBoxProperties _roundedBoxProps;
    private float _selectedInnerRadius = 0.1f;
    public bool isSelected;
    public Axis axis;

    private void Awake()
    {
        _roundedBoxProps = transform.GetComponentInChildren<RoundedBoxProperties>();
    }

    public void ToggleSelectedText()
    {
        isSelected = !isSelected;
        _roundedBoxProps.BorderInnerRadius = (isSelected) ? _selectedInnerRadius : 0;
    }
}
