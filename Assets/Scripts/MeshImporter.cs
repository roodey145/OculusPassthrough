using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MeshImporter : MonoBehaviour
{
    private Mesh[] meshes;


    public void LoadMesh(string fileName)
    {
        meshes = FileReader.ReadFile(fileName);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Gray");
        GetComponent<MeshFilter>().mesh = meshes[0];

    }
}
