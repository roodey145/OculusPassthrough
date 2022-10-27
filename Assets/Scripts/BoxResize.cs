using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoxResize : MonoBehaviour
{
    [SerializeField] private SphereCollider[] rightHandFingers;
    [SerializeField] private SphereCollider[] leftHandFingers;
    [SerializeField] private OVRHand[] hands;
    private Vector3 startSize;
    private bool isResizing;
    private bool firstResize = false;
    private float lastDistance;
    float radius = 0.03f;

    Vector3 extraScale = Vector3.zero;

    private void Start()
    {
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

        if (isResizing)
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
        if (!isResizing)
        {
            bool lefthandPinch = Vector3.Distance(rightHandFingers[0].transform.position, rightHandFingers[1].transform.position) <= radius;
            bool righthandPinch = Vector3.Distance(leftHandFingers[0].transform.position, leftHandFingers[1].transform.position) <= radius;
            isResizing = lefthandPinch && righthandPinch;
            firstResize = true;
        }
        else
        {
            bool lefthandPinch = Vector3.Distance(rightHandFingers[0].transform.position, rightHandFingers[1].transform.position) <= radius;
            bool righthandPinch = Vector3.Distance(leftHandFingers[0].transform.position, leftHandFingers[1].transform.position) <= radius;
            isResizing = lefthandPinch && righthandPinch;
        }

    }
}
