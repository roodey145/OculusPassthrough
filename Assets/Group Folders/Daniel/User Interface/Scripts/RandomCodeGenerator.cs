using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomCodeGenerator : MonoBehaviour
{
    private TextMeshProUGUI _codeText;
    private void Awake()
    {
        _codeText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _codeText.text = $"Meeting Code: {Random.Range(1000, 9999)}";
    }

}
