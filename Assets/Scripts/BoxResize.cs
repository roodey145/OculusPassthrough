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
            transform.localScale = Vector3.one * (handDistance - lastDistance) + startSize;
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
