using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectSize : MonoBehaviour
{
    public TextMeshProUGUI textValueX;
    public TextMeshProUGUI textValueY;
    public TextMeshProUGUI textValueZ;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        textValueX.text = transform.localScale.x.ToString();
        textValueY.text = transform.localScale.y.ToString();
        textValueZ.text = transform.localScale.z.ToString();
        
        transform.localScale = new Vector3(_collider.size.x, _collider.size.y, _collider.size.z);
    }
}
