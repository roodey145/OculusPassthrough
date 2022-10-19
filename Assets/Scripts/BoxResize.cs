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

    private float[] distances = new float[3];
    private const int COUNTER_MAX = 2;
    private const int COUNTER_MIN = 0;
    private int _counter = 0;
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
        float handDistance = Vector3.Distance(hands[0].transform.position, hands[1].transform.position);
        distances[Counter] = handDistance;
        Counter++;
        /*
        if (!distances.Contains(default(float)))
        {
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = default(float);
            }
        }

        // Check for any new flex
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] == default(float))
            {
                distances[0] = handDistance;
                break; // We want only the hand distance for this current frame in the update so we break.
            }
        }
        */

        if (isResizing)
        {
            transform.localScale = ((Vector3.one * GetSizingDirection()) * handDistance) + startSize;
        }
    }

    private int GetSizingDirection()
    {
        for (int i = 0; i < distances.Length - 1; i++)
        {
            if (distances[i] == default(float)) continue;

            if (distances[i] < distances[i + 1])
            {
                return -1;
            }
        }
        return 1;
    }
    public void ResizeBox()
    {
        isResizing = true;
        Debug.Log("Resizing");
        startSize = transform.localScale;
    }

    public void StopResizingBox()
    {
        isResizing = false;
        Debug.Log("Stopped Resizing");
    }
}
