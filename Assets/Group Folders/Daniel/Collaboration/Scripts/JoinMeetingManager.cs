using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinMeetingManager : MonoBehaviour
{
    private Network _network;

    private InputFieldData[] _inputFieldData;
    private InputFieldData _meetingCodeInputField;
    private Button _joinMeetingButton;

    void Start()
    {
        _network = FindObjectOfType<Network>();
        _inputFieldData = GetComponentsInChildren<InputFieldData>();
        foreach (InputFieldData inputFieldData in _inputFieldData)
        {
            switch (inputFieldData.m_inputFieldType)
            {
                case InputFieldType.MeetingCode:
                    _meetingCodeInputField = inputFieldData;
                    break;
            }
        }
    }

    private void OnEnable()
    {
        _joinMeetingButton.onClick.AddListener(JoinMeeting);
    }

    private void OnDisable()
    {
        _joinMeetingButton.onClick.RemoveListener(JoinMeeting);
    }

    public void ShowJoinMeetingSuccess()
    {
        // TODO - Show like the other success
    }

    public void ShowWrongMeetingCodeMessage()
    {

    }

    public void ShowMissingMeetingCodeMessage()
    {

    }

    private void JoinMeeting()
    {
        _network.JoinMeeting(_meetingCodeInputField.m_inputField.text);
    }


}
