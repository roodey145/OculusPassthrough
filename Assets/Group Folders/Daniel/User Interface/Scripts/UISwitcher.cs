using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISwitcher : MonoBehaviour
{
    [SerializeField] private HandMenuItem _handMenuItem;
    [SerializeField] private bool m_leadsToNothing;
    [SerializeField] private GameObject m_currentUI;
    [SerializeField] private GameObject m_nextUI;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void SwitchUI()
    {
        m_currentUI.SetActive(false);
        if (m_leadsToNothing)
        {
            if (_handMenuItem != null)
            {
                _handMenuItem.m_isSelected = false;
                _handMenuItem.UpdateSelection();
            }
            return;
        }
        m_nextUI.SetActive(true);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => StartCoroutine(UISwitch()));
    }

    private IEnumerator UISwitch()
    {
        yield return new WaitForSeconds(0.1f);
        SwitchUI();
    }
}
