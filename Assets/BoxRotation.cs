using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotation : MonoBehaviour
{
    [SerializeField]
    private OVRHand hand;
    private float startRotationY;
    private bool isRotating;
    private bool firstRotation;
    private float lastRotation;

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up * hand.transform.position.x, Space.Self);
            startRotationY = transform.position.y;
            lastRotation = hand.transform.position.y;
        }
    }

    public void RotateBox()
    {
        Debug.Log("Rotating Box");
        isRotating = true;
    }

    public void StopRotateBox()
    {
        Debug.Log("Stopped rotating box");
        isRotating = false;
    }
}
