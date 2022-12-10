using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class ManipulationChecker : MonoBehaviour
{
    private HandGrabInteractable touchHandGrabInteractable;
    private Network _network;
    private Vector3 scale;
    private Vector3 position;
    private Vector3 rotation;

    void Start()
    {
        _network = FindObjectOfType<Network>();
        touchHandGrabInteractable = GetComponent<HandGrabInteractable>();
        scale = transform.localScale;
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
        //Time.fixedDeltaTime = 1 / 20;
    }

    void FixedUpdate()
    {
        if (UserInfo.instance == null) return;

        if (!UserInfo.instance.isMeetingHost && _network.IsRetrivingInfo)
        {
            transform.position = ModelInfo.instance.position;
            transform.rotation = Quaternion.Euler(ModelInfo.instance.rotation);
            transform.localScale = ModelInfo.instance.scale;
        }

        if ((IsScaleManipulated() || IsPositionManipulated() || IsRotationManipulated()) && UserInfo.instance.isMeetingHost)
        {
            _network.UpdateModelInfo(transform.position, transform.localScale, transform.eulerAngles);
            scale = transform.localScale;
            position = transform.position;
            rotation = transform.rotation.eulerAngles;
            print("Updated Model Info");
        }
    }

    private bool IsScaleManipulated()
    {
        return transform.localScale - scale != Vector3.zero;
    }

    private bool IsPositionManipulated()
    {
        return transform.position - position != Vector3.zero;
    }

    private bool IsRotationManipulated()
    {
        return transform.rotation.eulerAngles - rotation != Vector3.zero;
    }

    private IEnumerator WaitForServer()
    {
        yield return new WaitForSeconds(4);
        if (!UserInfo.instance.isMeetingHost) touchHandGrabInteractable.enabled = false;
        _network.UpdateModelInfo(position, scale, rotation);
    }
}
