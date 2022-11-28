using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using TMPro;
using UnityEngine;

public class HandMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject m_uiMenu;
    [SerializeField] private GameObject m_axis;
    [SerializeField] private TextMeshPro m_descriptionText;
    private PokeInteractable _pokeInteractable;
    private RoundedBoxProperties _roundedBoxProperties;
    private float _selectionBorderRadius = 0.05f;
    
    public bool _isSelected
    {
        private set;
        get;
    }

    private void Awake()
    {
        _pokeInteractable = GetComponent<PokeInteractable>();
        _roundedBoxProperties = GetComponentInChildren<RoundedBoxProperties>();
    }

    private void OnEnable()
    {
        var descText = m_descriptionText.gameObject;
        descText.SetActive(false);
        _pokeInteractable.WhenStateChanged += ToggleMenus;
    }

    private void OnDisable()
    {
        _pokeInteractable.WhenStateChanged -= ToggleMenus;
    }

    private void ToggleMenus(InteractableStateChangeArgs args)
    {
        if (args.NewState != InteractableState.Select) return;
        _isSelected = !_isSelected;
        m_uiMenu.SetActive(!m_uiMenu.activeInHierarchy);
        m_axis.SetActive(!m_axis.activeInHierarchy);
        SetSelectVisibility(_isSelected);
    }
    
    private void SetSelectVisibility(bool state)
    {
        _roundedBoxProperties.BorderInnerRadius = (state) ? _selectionBorderRadius : 0;
    }
    
    private void ToggleDescriptionTextVisibility()
    {
        var descText = m_descriptionText.gameObject;
        descText.SetActive(!descText.activeInHierarchy);
    }

    private void OnTriggerEnter(Collider other)
    {
        ToggleDescriptionTextVisibility();
    }

    private void OnTriggerExit(Collider other)
    {
        ToggleDescriptionTextVisibility();
    }
}
