using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    internal static UserInfo instance { private set; get; }
    internal bool loggedIn { private set; get; }
    internal bool hasCreatedMeeting;
    internal bool hasJoinedMeeting;

    internal FileHeader[] fileHeaders;


    private UserInfo(bool loggedIn)
    {
        this.loggedIn = loggedIn;
    }


    internal static void CreateInstance(bool loggedIn)
    {
        //lock(instance)
        //{
        if (instance == null && loggedIn)
        {
            instance = new UserInfo(loggedIn);
        }
        //}
    }
}
