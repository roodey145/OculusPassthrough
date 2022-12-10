using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private bool isHost;
    [SerializeField] private TMP_InputField m_inputField;
    [SerializeField] private Button m_confirmCodeButton;
    [SerializeField] private Button m_hideButton;
    [SerializeField] private Toggle m_isHostToggle;
    [SerializeField] private Button m_confirmHostButton;
    private Network _network;
    void Start()
    {
        _network = FindObjectOfType<Network>();
        _network.Login("Roodey145", "Roodey145");
    }

    private void OnEnable()
    {
        m_confirmCodeButton.onClick.AddListener(JoinMeeting);
        m_hideButton.onClick.AddListener(HideUI);
        m_confirmHostButton.onClick.AddListener(MeetingRole);
    }

    private void OnDisable()
    {
        m_confirmCodeButton.onClick.RemoveListener(JoinMeeting);
        m_hideButton.onClick.RemoveListener(HideUI);
        m_confirmHostButton.onClick.RemoveListener(MeetingRole);
    }

    private IEnumerator WaitForServer()
    {
        yield return new WaitForSeconds(4);
        JoinMeeting();
    }

    private void HideUI()
    {
        m_confirmCodeButton.transform.parent.gameObject.SetActive(false);
    }

    private void MeetingRole()
    {
        if (m_isHostToggle) CreateMeeting();
        else JoinMeeting();
    }

    private void CreateMeeting()
    {
        _network.CreateMeeting("Admin", 2);
    }

    private void JoinMeeting()
    {
        //_network.JoinMeeting("4204a6");
        _network.JoinMeeting(m_inputField.text);
    }
}
