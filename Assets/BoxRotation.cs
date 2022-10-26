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

        if (isRotating)
        {
            switch (HandMenuController.controlledAxis)
            {
                case Axis.X:
                    transform.rotation *= Quaternion.Euler((fingertip.position.x - lastPosition) * rotationSpeed, 0, 0);
                    break;
                case Axis.Y:
                    transform.rotation *= Quaternion.Euler(0, (fingertip.position.x - lastPosition) * rotationSpeed, 0);
                    break;
                case Axis.Z:
                    transform.rotation *= Quaternion.Euler(0, 0, (fingertip.position.x - lastPosition) * rotationSpeed);
                    break;
            }
            lastPosition = fingertip.position.x;
        }
    }

    public void RotateBox()
    {
        isRotating = true;
        lastPosition = fingertip.position.x;
    }

    public void StopRotateBox()
    {
        isRotating = false;
    }
}
