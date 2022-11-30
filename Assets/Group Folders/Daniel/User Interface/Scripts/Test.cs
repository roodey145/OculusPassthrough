using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Test : MonoBehaviour 
{
    private TMP_InputField[] _inputFields;
    private int selectedInputFieldIndex;
    private bool isKeyDown;
    private Keyboard _keyboard;
    private Coroutine _coroutine;
    private void Awake()
    {
        _inputFields = GetComponentsInChildren<TMP_InputField>();
        StartCoroutine(WaitForKeyboard());
    }

    private void OnEnable()
    {
        for (int i = 0; i < _inputFields.Length; i++)
        {
            var i1 = i;
            _inputFields[i].onSelect.AddListener(delegate { OnSelect(i1); });
        }
    }

    private void OnDisable()
    {
        foreach (var inputField in _inputFields)
        {
            inputField.onSelect.RemoveAllListeners();
        }
    }
    private void OnSelect(int index)
    {
        Debug.Log("Selected " + index);
        selectedInputFieldIndex = index;
        _inputFields[selectedInputFieldIndex].caretBlinkRate = 0.4f;
    }

    private void FixedUpdate()
    {
        if (_coroutine != null) return;

        _inputFields[selectedInputFieldIndex].text += Input.inputString;
        _coroutine = StartCoroutine(WaitForKeyboard());

    }

    IEnumerator WaitForKeyboard()
    {
        yield return new WaitForSeconds(0.5f);
        _coroutine = null;
    }


}
