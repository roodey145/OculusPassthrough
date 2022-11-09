using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
public class MeshTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject mesh = UnityMeshImporter.MeshImporter.Load(Application.persistentDataPath + "/NVIDIA.fbx", 0.001f, 0.001f, 0.001f);
        mesh.AddComponent<BoxCollider>();
        Grabbable grabbable = mesh.AddComponent<Grabbable>();
        Rigidbody rb = mesh.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        HandGrabInteractable grab = mesh.AddComponent<HandGrabInteractable>();
        grab.InjectOptionalPointableElement(grabbable);
        grab.InjectRigidbody(rb);
    }
}
