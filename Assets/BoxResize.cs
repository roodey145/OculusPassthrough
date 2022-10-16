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

    private void Update()
    {
        if (isResizing)
        {
            float initalDistance = Vector3.Distance(hands[0].transform.position, hands[1].transform.position);
            transform.localScale = (Vector3.one * GetSizingDirection()) * initalDistance;
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
