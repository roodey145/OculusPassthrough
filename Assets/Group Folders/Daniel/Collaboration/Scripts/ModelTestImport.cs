using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.Stl;

public class ModelTestImport : MonoBehaviour
{
    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
        Mesh[] mesh = Importer.Import("C:\\Users\\Daniel Lerche\\Documents\\Unity\\benchy.stl", CoordinateSpace.Left, UpAxis.Y, true, UnityEngine.Rendering.IndexFormat.UInt32);
        GetComponent<MeshFilter>().mesh = mesh[0];
    }
}
