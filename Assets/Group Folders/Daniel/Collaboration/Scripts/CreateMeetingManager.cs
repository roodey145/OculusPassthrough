using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMeetingManager : MonoBehaviour
{
    [SerializeField] private Button _leaveMeetingButton;
    private Network _network;
    private RandomCodeGenerator _randomCodeGenerator;

    void Start()
    {
        _network = FindObjectOfType<Network>();
        _randomCodeGenerator = GetComponentInChildren<RandomCodeGenerator>();
        StartCoroutine(Test());
    }

    private void OnEnable()
    {
        _leaveMeetingButton.onClick.AddListener(LeaveMeeting);
    //    if (!UserInfo.instance.hasCreatedMeeting) CreateMeeting();
    }

    private void OnDisable()
    {
        _leaveMeetingButton.onClick.RemoveListener(LeaveMeeting);
    }

    private void LeaveMeeting()
    {
        // TODO - Leave meeting
    }

    public void CreateMeeting()
    {
        // TODO - Get the selected file id

        //_network.CreateMeeting(_randomCodeGenerator.meetingRoomCode.ToString(), UserInfo.instance.fileHeaders[0].id);
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        
    }
}
