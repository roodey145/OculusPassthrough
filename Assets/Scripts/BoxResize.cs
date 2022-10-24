using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoxResize : MonoBehaviour
{
    [SerializeField] private OVRHand[] hands;
    private Vector3 startSize;
    private bool isResizing;
    private bool firstResize = false;
    private int activeHands;
    private float[] distances = new float[5];
    private const int COUNTER_MAX = 4;
    private const int COUNTER_MIN = 0;
    private int _counter = 0;
    private float lastDistance;
    public int Counter
    {
        get
        {
            return _counter;
        }
        private set
        {
            _counter = (value > COUNTER_MAX) ? COUNTER_MIN : value;
        }
    }

    private void Start()
    {
        startSize = transform.localScale;
    }

    private void Update()
    {
        isResizing = activeHands == 2;

        float handDistance = Vector3.Distance(hands[0].transform.position, hands[1].transform.position);
        distances[Counter] = handDistance;
        if (firstResize)
        {
            lastDistance = handDistance;
            firstResize = false;
        }
        Counter++;

        if (isResizing)
        {
            transform.localScale = Vector3.one * (handDistance - lastDistance) + startSize;
            startSize = transform.localScale;
            lastDistance = handDistance;
        }
    }
    public void ResizeBox()
    {
        firstResize = true;
        activeHands++;
        Debug.Log("Resizing");
        startSize = transform.localScale;
    }

    public void StopResizingBox()
    {
        activeHands--;
        Debug.Log("Stopped Resizing");
    }
}
