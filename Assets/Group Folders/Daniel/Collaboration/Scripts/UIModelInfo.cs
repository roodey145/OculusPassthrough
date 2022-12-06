using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelInfo : MonoBehaviour
{
    public int id;
    public string name;
    public string date;
    public string path;

    public void setData(int id, string name, string date, string path)
    {
        this.id = id;
        this.name = name;
        this.date = date;
        this.path = path;
    }
}
