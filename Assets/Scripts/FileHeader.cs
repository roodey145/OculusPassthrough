using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileHeader
{
    private const int RAWDATA_LENGTH = 3;
    internal int id;
    internal string name;
    internal string date;
    internal string path;

    internal FileHeader(int id, string name, string date)
    {
        this.id = id;
        this.name = name;
        this.date = date;
    }
    
    /// <summary>
    /// Constructor which uses data in the following manner --> id,name,dato 
    /// to create a new FileHeader.
    /// </summary>
    /// <param name="rawData">The data in the following manner id,name,dato.</param>
    internal FileHeader(string rawData)
    {
        string[] data = rawData.Split(',');

        if(data.Length == RAWDATA_LENGTH)
        {
            id = int.Parse( data[0] );
            name = data[1];
            date = data[2];
        }
        else
        {
            // Should be deleted
            MonoBehaviour.print(rawData);
        }
    }

}
