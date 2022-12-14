using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoxResize : MonoBehaviour
{
    public SphereCollider[] rightHandFingers;
    public SphereCollider[] leftHandFingers;
    public OVRHand[] hands;
    private Mesh mesh;
    private Vector3 startSize;
    public bool IsResizing
    {
        private set;
        get;
    }
    private bool firstResize = false;
    private float lastDistance;
    float radius = 0.015f;

    Vector3 extraScale = Vector3.zero;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        startSize = transform.localScale;
    }

    private void Update()
    {
        float handDistance = Vector3.Distance(hands[0].transform.position, hands[1].transform.position);

        if (firstResize)
        {
            lastDistance = handDistance;
            firstResize = false;
        }

        if (IsResizing)
        {
            // switch (HandMenuController.controlledAxis)
            // {
            //     case Axis.X:
            //         extraScale.x = 1;
            //         extraScale.y = 0;
            //         extraScale.z = 0;
            //         break;
            //     case Axis.Y:
            //         extraScale.x = 0;
            //         extraScale.y = 1;
            //         extraScale.z = 0;
            //         break;
            //     case Axis.Z:
            //         extraScale.x = 0;
            //         extraScale.y = 0;
            //         extraScale.z = 1;
            //         break;
            // }

            Vector3 scale = Vector3.one * (handDistance - lastDistance) + startSize;
            if (scale.x <= 0 || scale.y <= 0 || scale.z <= 0) scale = Vector3.one * 0.05f;
            transform.localScale = scale;
            startSize = transform.localScale;
            lastDistance = handDistance;
        }
    }

    private void FixedUpdate()
    {
        if (!IsResizing)
        {
            bool lefthandPinch = Vector3.Distance(rightHandFingers[0].transform.position, rightHandFingers[1].transform.position) <= radius;
            bool righthandPinch = Vector3.Distance(leftHandFingers[0].transform.position, leftHandFingers[1].transform.position) <= radius;
            IsResizing = lefthandPinch && righthandPinch;
            firstResize = true;
        }
        else
        {
            bool lefthandPinch = Vector3.Distance(rightHandFingers[0].transform.position, rightHandFingers[1].transform.position) <= radius;
            bool righthandPinch = Vector3.Distance(leftHandFingers[0].transform.position, leftHandFingers[1].transform.position) <= radius;
            IsResizing = lefthandPinch && righthandPinch;
        }

    }
}
