using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private MeshImporter importer;
    [SerializeField]
    private GameObject fileButton;
    private FileInfo[] fileNames;
    void Start()
    {
        fileNames = new DirectoryInfo(Application.persistentDataPath).GetFiles();
        foreach (FileInfo file in fileNames)
        {
            GameObject btn = Instantiate(fileButton, transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = file.Name;
            btn.GetComponent<Button>().onClick.AddListener(() => importer.LoadMesh(file.Name));
        }
    }
}
