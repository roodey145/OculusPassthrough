using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JoinMeetingManager : MonoBehaviour
{
    [SerializeField] private Button m_joinMeetingButton;
    [SerializeField] private float m_messageDisplayDuration;
    private Network _network;
    private InputFieldData[] _inputFieldData;
    private InputFieldData _meetingCodeInputField;
    private GameObject[] _userInterfaces;

    void Start()
    {
        _userInterfaces = Resources.LoadAll<GameObject>("UserInterfaces");
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
        m_joinMeetingButton.onClick.AddListener(JoinMeeting);
    }

    private void OnDisable()
    {
        m_joinMeetingButton.onClick.RemoveListener(JoinMeeting);
    }

    public void ShowJoinMeetingSuccess()
    {
        GameObject joinMeetingUI = GetUserInterface("JoinMeetingSuccess");
        StartCoroutine(ShowMessageTemporarily(joinMeetingUI));
    }

    public void ShowWrongMeetingCodeMessage()
    {
        _meetingCodeInputField.m_errorMessage.text = "Wrong meeting code!";
        _meetingCodeInputField.m_inputField.image.color = Color.red;
    }

    public void ShowMissingMeetingCodeMessage()
    {
        _meetingCodeInputField.m_errorMessage.text = "Missing meeting code!";
        _meetingCodeInputField.m_inputField.image.color = Color.red;
    }

    private void JoinMeeting()
    {
        _network.JoinMeeting("4aebfd");
        
        //_network.JoinMeeting(_meetingCodeInputField.m_inputField.text);
    }
    
    public void ClearAllErrorMessages()
    {
        _meetingCodeInputField.m_errorMessage.text = "";
        _meetingCodeInputField.m_inputField.image.color = _meetingCodeInputField.m_inputFieldNormalColor;
    }
    
    private GameObject GetUserInterface(string interfaceName)
    {
        return _userInterfaces.FirstOrDefault(userInterface => userInterface.name == interfaceName);
    }
    
    private IEnumerator ShowMessageTemporarily(GameObject userInterface)
    {
        GameObject ui = Instantiate(userInterface, transform.parent);
        yield return new WaitForSeconds(m_messageDisplayDuration);
        Destroy(ui);
        transform.parent.gameObject.SetActive(false);
    }
}
