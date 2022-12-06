using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using System.Linq;

public class ModelImporter : MonoBehaviour
{
    public static GameObject loadedModel
    {
        private set;
        get;
    }
    
    public static GameObject LoadModel(string path, Vector3 scale)
    {
        GameObject model = UnityMeshImporter.MeshImporter.Load(path, scale.x, scale.y, scale.z);
        model.AddComponent<TableSurfaceLimiter>();
        
        /*
        Rigidbody rb = model.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        model.AddComponent<BoxCollider>();
        Grabbable grabbable = model.AddComponent<Grabbable>();
        
        HandGrabInteractable handGrabInteractable = model.AddComponent<HandGrabInteractable>();
        handGrabInteractable.InjectOptionalPointableElement(grabbable);
        handGrabInteractable.InjectRigidbody(rb);
        
        model.AddComponent<ManipulationChecker>();
        
        BoxResize boxResize = model.AddComponent<BoxResize>();
        boxResize.hands = FindObjectsOfType<OVRHand>();
        boxResize.leftHandFingers = GameObject.FindGameObjectsWithTag("LeftHand").Select(obj => obj.GetComponentInChildren<SphereCollider>()).ToArray();
        boxResize.leftHandFingers = GameObject.FindGameObjectsWithTag("RightHand").Select(obj => obj.GetComponentInChildren<SphereCollider>()).ToArray();;
        */
        loadedModel = model;
        return model;
    }
}
