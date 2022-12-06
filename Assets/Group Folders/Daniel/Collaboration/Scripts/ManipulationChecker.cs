using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationChecker : MonoBehaviour
{

    private Network _network;
    private Vector3 scale;
    private Vector3 position;
    private Vector3 rotation;

    void Start()
    {
        _network = FindObjectOfType<Network>();
        scale = transform.lossyScale;
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        if (IsScaleManipulated() || IsPositionManipulated() || IsRotationManipulated())
        {
            _network.UpdateModelInfo(transform.position, transform.lossyScale, transform.eulerAngles);
            scale = transform.lossyScale;
            position = transform.position;
            rotation = transform.rotation.eulerAngles;
        }
    }

    private bool IsScaleManipulated()
    {
        return transform.lossyScale != scale;
    }

    private bool IsPositionManipulated()
    {
        return transform.position != position;
    }

    private bool IsRotationManipulated()
    {
        return transform.rotation.eulerAngles != rotation;
    }
}
