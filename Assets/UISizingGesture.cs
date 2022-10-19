using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISizingGesture : MonoBehaviour
{
    [SerializeField]
    private float handsTriggerDistance;
    [SerializeField]
    private OVRHand[] hand;
    private bool isGesturing;
    void Update()
    {
        if(isGesturing)
        {
            float distance = Vector3.Distance(hand[0].transform.position, hand[1].transform.position);
            if (distance <= handsTriggerDistance)
            {
                Debug.Log("Resizing");
            }
        }
    }

    public void HandGestureSelection()
    {
        isGesturing = true;
    }
    public void HandGestureUnSelection()
    {
        isGesturing = false;
    }
}
