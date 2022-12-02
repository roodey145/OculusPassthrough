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
    private bool shouldBeUpperCase;
    private bool shouldBeUpperCasePersistent;
    private void Awake()
    {
        _inputFields = GetComponentsInChildren<TMP_InputField>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _inputFields.Length; i++)
        {
            var i1 = i;
            _inputFields[i].onSelect.AddListener(delegate { OnSelect(i1); });
        }
        StartCoroutine(_FixedUpdate());
    }

    private void OnDisable()
    {
        foreach (var inputField in _inputFields)
        {
            inputField.onSelect.RemoveAllListeners();
        }
        StopCoroutine(_FixedUpdate());
    }
    private void OnSelect(int index)
    {
        Debug.Log("Selected " + index);
        selectedInputFieldIndex = index;
    }

    private IEnumerator _FixedUpdate()
    {
        // if (_coroutine != null) return;
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            shouldBeUpperCase = e.capsLock;
            if (e.shift) shouldBeUpperCase = !shouldBeUpperCase;

            if (e.isKey)
            {
                TMP_InputField selectedInputField = _inputFields[selectedInputFieldIndex];
                switch (e.keyCode)
                {
                    case KeyCode.Backspace:
                        if (selectedInputField.text.Length <= 0) break;
                        selectedInputField.text = selectedInputField.text[..^1];
                        break;
                    case KeyCode.Space:
                        selectedInputField.text += " ";
                        break;
                    case KeyCode.LeftShift:
                        shouldBeUpperCase = true;
                        break;
                    case KeyCode.Comma:
                        selectedInputField.text += ",";
                        break;
                    case KeyCode.Period:
                        selectedInputField.text += ".";
                        break;
                    default:
                        if (e.keyCode.ToString().StartsWith("Alpha")) // The number keys starts with Alpha and the actual number.
                        {
                            // Get the end of the the string to only get the number
                            selectedInputField.text += e.keyCode.ToString()[^1];
                            break;
                        }
                        if (shouldBeUpperCase)
                        {
                            selectedInputField.text += e.keyCode.ToString().ToUpper();
                            break;
                        }
                        selectedInputField.text += e.keyCode.ToString().ToLower();
                        break;
                }


                /*
                bool specialKey = true;
                TMP_InputField selectedInputField = _inputFields[selectedInputFieldIndex];
                int caretPos = _inputFields[selectedInputFieldIndex].caretPosition;
                string inputFieldText = selectedInputField.text;
                char keyChar = selectedInputField.text.ToCharArray()[0];

                switch (e.keyCode)
                {
                    case KeyCode.Backspace:
                        selectedInputField.SetTextWithoutNotify(inputFieldText[..^1]);
                        break;
                    case KeyCode.Space:
                        selectedInputField.text += " ";
                        break;
                    default:
                        specialKey = false;
                        break;

                }
                if (!specialKey)
                {
                    selectedInputField.text += e.keyCode.ToString().ToLower();
                }
                */
                selectedInputField.caretPosition = selectedInputField.text.Length - 1;
            }

        }
        yield return new WaitForSeconds(0.001f);

        StartCoroutine(_FixedUpdate());

    }
}
