using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject wristUI;
    public void ShowWristUI()
    {
        wristUI.SetActive(true);
    }
    public void CloseWristUI()
    { 
        wristUI.SetActive(false);
    }
}
