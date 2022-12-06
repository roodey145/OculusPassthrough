using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class RandomCodeGenerator : MonoBehaviour
{
    [SerializeField] private Button m_leaveButton;
    public int meetingRoomCode
    {
        private set;
        get;
    }
    private TextMeshProUGUI _codeText;

    private void Awake()
    {
        meetingRoomCode = Random.Range(1000, 9999);
        _codeText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        m_leaveButton.onClick.AddListener(ResetRandomNumber);
        _codeText.text = $"Meeting Code: {meetingRoomCode}";
    }

    private void OnDisable()
    {
        m_leaveButton.onClick.RemoveListener(ResetRandomNumber);
    }

    private void ResetRandomNumber()
    {
        meetingRoomCode = Random.Range(1000, 9999);
    }

}
