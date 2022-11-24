using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum Axis : byte
{
    X = 0,
    Y = 1,
    Z = 2
}

public class HandMenuController : MonoBehaviour
{
    public static Axis controlledAxis = Axis.Y;
    private GameObject[] _axisIndicators = new GameObject[3];
    [SerializeField] AxisButton[] m_axisButtons;

    [SerializeField]
    private float _outFocus = 0.15f;
    [SerializeField]
    private float _inFocus = 0.9f;

    private void Awake()
    {
        _axisIndicators[0] = GameObject.Find("xAxis");
        _axisIndicators[1] = GameObject.Find("yAxis");
        _axisIndicators[2] = GameObject.Find("zAxis");

        if (_axisIndicators.Contains(null)) return;
        
        for (int i = 0; i < _axisIndicators.Length; i++)
        {
            _ChangeOpacity(_axisIndicators[i], _outFocus);
            // _axisIndicators[i].SetActive(false);
        }
        _ChangeOpacity(_axisIndicators[(int)controlledAxis], _inFocus);
        // _axisIndicators[(int)controlledAxis].SetActive(true);
    }

    private void Start()
    {
        UpdateSelectedTextUI();
    }


    private void _ChangeOpacity(GameObject obj, float opacity)
    {
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        Color color = mesh.material.color;
        color.a = opacity;
        mesh.material.color = color;
    }


    public void LockAxis(int axisToControl)
    {
        controlledAxis = (Axis)axisToControl;
        UpdateSelectedTextUI();
        if (_axisIndicators.Contains(null)) return;
        _ChangeOpacity(_axisIndicators[(int)controlledAxis], _outFocus);
        // _axisIndicators[(int)controlledAxis].SetActive(false);
        
        _ChangeOpacity(_axisIndicators[axisToControl], _inFocus);
        // _axisIndicators[axisToControl].SetActive(true);
        
    }

    private void UpdateSelectedTextUI()
    {
        foreach (var item in m_axisButtons)
        {
            if (item.axis == controlledAxis)
            {
                item.isSelected = true;
                item.ToggleSelectedText();
                continue;
            }
            item.isSelected = false;
            item.ToggleSelectedText();
        }
    }
}
