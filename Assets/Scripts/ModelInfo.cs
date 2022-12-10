using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ModelManipulatable
{
    Rotation,
    Scale,
    Position
}

public class ModelInfo
{
    internal static ModelInfo instance;
    internal Vector3 rotation;
    internal Vector3 scale;
    internal Vector3 position;


    internal static void RegisterModelInfo(string rawData)
    {
        if (instance == null)
        {
            new ModelInfo(rawData);
        }
        else
        {
            string[] data = rawData.Split(';');

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != "")
                {
                    _AssignAnyInfo(data[i]);
                }
            }
        }
        /*
        // Print the model info
        MonoBehaviour.print("Position: " + instance.position);
        MonoBehaviour.print("Scale: " + instance.scale);
        MonoBehaviour.print("Rotation: " + instance.rotation);
        */

    }

    private ModelInfo(string rawData)
    {
        instance = this;
        string[] data = rawData.Split(';');

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] != "")
            {
                _AssignAnyInfo(data[i]);
            }
        }
    }

    private ModelInfo(Vector3 position, Vector3 scale, Vector3 rotation)
    {
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }


    private static void _AssignAnyInfo(string data)
    {
        //string[] featureData;
        if (data.Contains(ModelManipulatable.Position.ToString()))
        { // The rotation data
            _AssignSpecificInfo(ref instance.position, data);
        }
        else if (data.Contains(ModelManipulatable.Scale.ToString()))
        { // The rotation data

            _AssignSpecificInfo(ref instance.scale, data);
        }
        else if (data.Contains(ModelManipulatable.Rotation.ToString()))
        { // The rotation data

            _AssignSpecificInfo(ref instance.rotation, data);
            //featureData = data.Split(",");
            //// Makes sure there is enough info
            //if(featureData.Length < 4)
            //{
            //    MonoBehaviour.print(data);
            //    return;
            //}
            //// The first cell is "Rotation"
            //// The second cell is x rotation
            //rotation.x = int.Parse(featureData[1]);
            //rotation.y = int.Parse(featureData[2]);
            //rotation.z = int.Parse(featureData[3]);
        }
    }

    private static void _AssignSpecificInfo(ref Vector3 featureToAssign, string rawData)
    {
        string[] featureData;
        featureData = rawData.Split(",");
        // Makes sure there is enough info
        if (featureData.Length < 4)
        {
            MonoBehaviour.print(rawData);
            return;
        }
        // The first cell is "Manipulatable"
        // The second cell is x, 3rd y, 4th z
        // featureToAssign.x = float.Parse(featureData[1], System.Globalization.NumberStyles.AllowDecimalPoint);
        //featureToAssign.x = float.Parse(featureData[1].Replace(".", ","));
        //featureToAssign.y = float.Parse(featureData[2].Replace(".", ","));
        //featureToAssign.z = float.Parse(featureData[3].Replace(".", ","));

        featureToAssign.x = float.Parse(featureData[1]);
        featureToAssign.y = float.Parse(featureData[2]);
        featureToAssign.z = float.Parse(featureData[3]);

        // featureToAssign.x = float.Parse(featureData[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture);
        // featureToAssign.y = float.Parse(featureData[2], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture);
        // featureToAssign.z = float.Parse(featureData[3], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture);
        Debug.Log($"Raw: {featureData[0]}: {featureData[1]}, {featureData[2]}, {featureData[3]}");
        Debug.Log($"Processed: {featureData[0]}: {featureToAssign.x}, {featureToAssign.y}, {featureToAssign.z}");
    }
}
