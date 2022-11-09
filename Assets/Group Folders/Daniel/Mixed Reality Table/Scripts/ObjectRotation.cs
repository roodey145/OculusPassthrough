using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float snapInterval;
    void Update()
    {
        Vector3 rotationAngle = transform.rotation.eulerAngles;

        if ((rotationAngle.x % snapInterval) != 0 || (rotationAngle.y % snapInterval) != 0 || (rotationAngle.z % snapInterval) != 0)
        {
            transform.rotation = GetSnapAngle();
        }
    }

    private Quaternion GetSnapAngle()
    {
        Vector3 rotationAngle = transform.rotation.eulerAngles;
        return Quaternion.Euler(Mathf.Round(rotationAngle.x / snapInterval) * snapInterval,
                                Mathf.Round(rotationAngle.y / snapInterval) * snapInterval,
                                Mathf.Round(rotationAngle.z / snapInterval) * snapInterval);
    }
}
