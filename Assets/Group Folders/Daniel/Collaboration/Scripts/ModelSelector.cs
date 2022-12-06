using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class ModelSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject m_filePrefab;

    [SerializeField] private Transform m_selectedDestination;

    private void OnEnable()
    {
        StartCoroutine(WaitForFileHeaders());
    }

    private void OnDisable()
    {
        
    }

    private void SetAsSelected(GameObject obj)
    {
        obj.transform.SetParent(m_selectedDestination);
        Button button = obj.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SetAsDeselected(obj));
    }
    
    private void SetAsDeselected(GameObject obj)
    {
        obj.transform.SetParent(transform);
        Button button = obj.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SetAsSelected(obj));
    }

    private void LoadFile(string path)
    {
        //GameObject model = UnityMeshImporter.MeshImporter.Load(path, 0.001f, 0.001f, 0.001f);
        //model.SetActive(!model.activeInHierarchy);
    }

    private IEnumerator WaitForFileHeaders()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var fileHeader in UserInfo.instance.fileHeaders)
        {
            GameObject fileButton = Instantiate(m_filePrefab, transform);
            fileButton.GetComponent<Button>().onClick.AddListener(() => SetAsSelected(fileButton));
            fileButton.GetComponentInChildren<TextMeshProUGUI>().text = fileHeader.name;
            fileButton.AddComponent<UIModelInfo>().setData(fileHeader.id, fileHeader.name, fileHeader.date, fileHeader.path);
        }
    }
}
