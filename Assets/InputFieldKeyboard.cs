using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldKeyboard : MonoBehaviour
{
    private TouchScreenKeyboard overlayKeyboard;
    private TMP_InputField _inputField;
    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Focused()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        _inputField.text = overlayKeyboard.text;
    }
}
