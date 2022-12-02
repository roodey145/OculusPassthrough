using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHands : MonoBehaviour
{
    private static readonly float handInnerAlphaThreshold_ = 0.08f;
    private static readonly float handOuterAlphaThreshold_ = 0.20f;
    private static readonly float maximumPassthroughHandsDistance_ = 0.18f;
    private static readonly float minimumModelHandsDistance_ = 0.11f;
    private OVRTrackedKeyboard _trackedKeyboard;

    private void Awake()
    {
        _trackedKeyboard = GetComponent<OVRTrackedKeyboard>();
    }
    
    private void LateUpdate()
    {
        OVRPlugin.GetSkeleton(OVRPlugin.SkeletonType.HandLeft, out var handSkeletonLeft);
        var leftHandDistance = GetHandDistanceToKeyboard(handSkeletonLeft);
        OVRPlugin.GetSkeleton(OVRPlugin.SkeletonType.HandRight, out var handSkeletonRight);
        var rightHandDistance = GetHandDistanceToKeyboard(handSkeletonRight);
        
        var handsIntensity = new OVRPlugin.InsightPassthroughKeyboardHandsIntensity
        {
            LeftHandIntensity =
                ComputeOpacity(leftHandDistance, handInnerAlphaThreshold_, handOuterAlphaThreshold_),
            RightHandIntensity =
                ComputeOpacity(rightHandDistance, handInnerAlphaThreshold_, handOuterAlphaThreshold_)
        };
        OVRPlugin.SetInsightPassthroughKeyboardHandsIntensity(_trackedKeyboard.PassthroughOverlay.layerId, handsIntensity);
    }
    
    private float ComputeOpacity(float distance, float innerThreshold, float outerThreshold)
    {
        return Mathf.Clamp((outerThreshold - distance) / (outerThreshold - innerThreshold), 0.0f, 1.0f);
    }
    
    private float GetHandDistanceToKeyboard(OVRPlugin.Skeleton skeleton)
    {

        var pinchPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_Index3].Pose.Position;
        var handPosition = skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Middle1].Pose.Position;
        var pinkyPosition = skeleton.Bones[(int) OVRSkeleton.BoneId.Hand_Pinky3].Pose.Position;

        return Mathf.Min(_trackedKeyboard.GetDistanceToKeyboard(pinchPosition.FromVector3f()),
            _trackedKeyboard.GetDistanceToKeyboard(handPosition.FromVector3f()),
            _trackedKeyboard.GetDistanceToKeyboard(pinkyPosition.FromVector3f()));
    }
}
