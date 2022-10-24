using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;

    public void SwitchUI()
    {
        gameObject.SetActive(false);
        UI.SetActive(true);
    }
}
