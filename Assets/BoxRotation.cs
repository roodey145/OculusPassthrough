using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotation : MonoBehaviour
{
    [SerializeField]
    private Transform fingertip;
    [SerializeField]
    private float rotationSpeed;
    private bool isRotating;
    private float lastPosition;
    private List<float> positions = new();

    private void Update()
    {
        if (positions.Count > 30) positions.Clear();
        positions.Add(fingertip.position.x);
        if (isRotating)
        {
            transform.rotation *= Quaternion.Euler(0, (fingertip.position.x - positions[0]) * rotationSpeed, 0);
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
