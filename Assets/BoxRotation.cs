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
            transform.rotation *= Quaternion.Euler(0, (fingertip.position.x - lastPosition) * rotationSpeed, 0);
            /* transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    transform.rotation * Quaternion.Euler(0, (fingertip.position.x - lastPosition) * rotationSpeed, 0),
                                                    0.1f); */
            // transform.rotation *= Quaternion.Slerp(Quaternion.Euler(0, 0, 0),
            //                                         Quaternion.Euler(0, (fingertip.position.x - lastPosition) * rotationSpeed, 0),
            //                                         0.1f);
            lastPosition = fingertip.position.x;
        }
    }

    public void RotateBox()
    {
        Debug.Log("Rotating Box");
        isRotating = true;
        lastPosition = fingertip.position.x;
    }

    public void StopRotateBox()
    {
        Debug.Log("Stopped rotating box");
        isRotating = false;
    }
}
