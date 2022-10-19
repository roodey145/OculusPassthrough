using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class HandCollisonTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        QuantumConsole.Instance.LogToConsole(other.gameObject.name);
    }
}
