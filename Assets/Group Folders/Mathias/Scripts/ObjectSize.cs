using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectSize : MonoBehaviour
{
    public TextMeshPro textValueX;
    public TextMeshPro textValueY;
    public TextMeshPro textValueZ;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        textValueX.text = $"Width: {transform.localScale.x * 100}cm";
        textValueY.text = $"Height: {transform.localScale.y * 100}cm";
        textValueZ.text = $"Length: {transform.localScale.z * 100}cm";
        
        transform.localScale = new Vector3(_collider.size.x, _collider.size.y, _collider.size.z);
    }
}
