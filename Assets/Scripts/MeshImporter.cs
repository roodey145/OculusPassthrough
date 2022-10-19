using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MeshImporter : MonoBehaviour
{
    private Mesh[] meshes;

    void Start()
    {
        meshes = FileReader.ReadFile("Ball.stl");
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Gray"); 
        GetComponent<MeshFilter>().mesh = meshes[0];
    }


}
