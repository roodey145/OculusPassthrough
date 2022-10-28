using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAxisUI : MonoBehaviour
{
    [SerializeField] private GameObject _handAxisUI;

    public void ShowHandAxisUI()
    {
        _handAxisUI.SetActive(true);
    }

    public void HideHandAxisUI()
    {
        _handAxisUI.SetActive(false);
    }

}
