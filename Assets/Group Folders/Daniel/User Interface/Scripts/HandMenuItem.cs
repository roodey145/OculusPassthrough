using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using TMPro;
using UnityEngine;

public class HandMenuItem : MonoBehaviour
{
    [SerializeField] private GameObject m_userInterface;
    [SerializeField] private PokeInteractable m_pokeInteractable;
    [SerializeField] private TextMeshPro m_descriptionText;
    public InteractableState _currentState;
    private RoundedBoxProperties _roundedBoxProperties;
    private float _selectionBorderRadius = 0.05f;
    
    public bool m_isSelected;

    private void Awake()
    {
        _roundedBoxProperties = GetComponentInChildren<RoundedBoxProperties>();
    }

    private void OnEnable()
    {
        var descText = m_descriptionText.gameObject;
        descText.SetActive(false);
        
        m_pokeInteractable.WhenStateChanged += HandleStateChanged;
        m_pokeInteractable.WhenStateChanged += OnSelected;
    }

    private void OnDisable()
    {
        m_pokeInteractable.WhenStateChanged -= HandleStateChanged;
        m_pokeInteractable.WhenStateChanged -= OnSelected;
    }

    private void HandleStateChanged(InteractableStateChangeArgs args) => _currentState = args.NewState;

    private void OnSelected(InteractableStateChangeArgs args)
    {
        if (args.NewState != InteractableState.Select) return;
        m_isSelected = !m_isSelected;
        UpdateSelection();
    }

    public void UpdateSelection()
    {
        SetSelectVisibility(m_isSelected);
        m_userInterface.SetActive(m_isSelected);
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

    public void SetSelected(bool state)
    {
        m_isSelected = state;
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
