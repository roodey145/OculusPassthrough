using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MeetingManager : MonoBehaviour
{
    private Network _network;
    public string _meetingId
    {
        private set;
        get;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }

    private string[] GetModelFileNames()
    {
        return Directory.GetFiles(Application.persistentDataPath).Where(name => name.EndsWith(".fbx")).ToArray();
    }

    private void LeaveMeeting()
    {

    }

    public void ClearAllErrorMessages()
    {

    }
}
