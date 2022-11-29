using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRBetterTrackedKeyboardHands : MonoBehaviour
{
    private OVRCameraRig _ovrCameraRig;
    private OVRHand[] _hands;
    void Start()
    {
        _ovrCameraRig = FindObjectOfType<OVRCameraRig>();
        _hands = FindObjectsOfType<OVRHand>();
    }
    
    void Update()
    {
        
    }
}
