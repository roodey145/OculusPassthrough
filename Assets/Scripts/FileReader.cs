using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Parabox.Stl;
public class FileReader
{
    public static Mesh[] ReadFile(string fileName)
    {
        return Importer.Import(Application.persistentDataPath + Path.DirectorySeparatorChar + fileName, CoordinateSpace.Left, UpAxis.Y, true, UnityEngine.Rendering.IndexFormat.UInt32);
    }
}
