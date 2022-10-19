using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject wristButton;

    [SerializeField]
    private GameObject wristUI;
    public void WristBtnClicked()
    {
        Debug.Log("Clicked");
        wristButton.SetActive(false);
        wristUI.SetActive(true);
    }
}
