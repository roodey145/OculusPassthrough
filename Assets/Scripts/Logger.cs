using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Logger : MonoBehaviour
{
    [SerializeField]
    private TextMesh consoleText;
    public static Logger Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void LogSetup()
    {
        LogInfo("Ready to be used!");
    }

    public void LogInfo(string info)
    {
        consoleText.text = info;
    }
}
