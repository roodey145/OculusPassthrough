using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Parabox.Stl;

public class CreateMeetingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _meetingCode;
    [SerializeField] private Button _leaveMeetingButton;
    [SerializeField] private Transform m_selectedModels;
    private Network _network;
    public string _code
    {
        set
        {
            _code = value;
            _meetingCode.text = value.ToString();
        }
        get
        {
            return _code;
        }
    }

    void Start()
    {
        _network = FindObjectOfType<Network>();

    }

    private void OnEnable()
    {
        _leaveMeetingButton.onClick.AddListener(LeaveMeeting);
        CreateMeeting();
    }

    private void OnDisable()
    {
        _leaveMeetingButton.onClick.RemoveListener(LeaveMeeting);
    }

    private void LeaveMeeting()
    {
        UserInfo.instance.hasCreatedMeeting = false;
        // TODO - Leave meeting
    }

    public void CreateMeeting()
    {
        // TODO - Get the selected file id
        //_network.CreateMeeting("Test2", UserInfo.instance.fileHeaders[2].id);
        //print("Id: " + UserInfo.instance.fileHeaders[2].id);
        //print("Created from path: " + UserInfo.instance.fileHeaders[2].path);
        Importer.Import(Application.persistentDataPath + "/" + "3.stl", CoordinateSpace.Left, UpAxis.Y, true, UnityEngine.Rendering.IndexFormat.UInt32);

    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(4);
        if (!UserInfo.instance.hasCreatedMeeting) UserInfo.instance.hasCreatedMeeting = true;
        if (UserInfo.instance.hasCreatedMeeting)
        {
            CreateMeeting();
        }
    }
}
