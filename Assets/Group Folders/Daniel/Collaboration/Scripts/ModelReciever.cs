using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ModelReciever : MonoBehaviour
{
    [SerializeField]
    private GameObject filePrefab;

    private void OnEnable()
    {
        foreach (var fileHeader in UserInfo.instance.fileHeaders)
        {
            GameObject fileButton = Instantiate(filePrefab, transform.parent);
            fileButton.GetComponent<Button>().onClick.AddListener(() => LoadFile(fileHeader.path));
        }
    }

    private void OnDisable()
    {

    }

    private string[] GetModelFileNames()
    {
        return Directory.GetFiles(Application.persistentDataPath).Where(name => name.EndsWith(".fbx")).ToArray();
    }

    private void LoadFile(string path)
    {
        GameObject model = UnityMeshImporter.MeshImporter.Load(path, 0.001f, 0.001f, 0.001f);
        model.SetActive(!model.activeInHierarchy);
    }
}
