using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnap : MonoBehaviour
{
    public bool snapIsTrue;
    public float gridSize = 1;
    public Vector2 moveAmount = Vector2.zero;

    public int angelTrigger = 15;
    public float currentXAngel = 0;
    public float currentYAngel = 0;
    public float currentZAngel = 0;
    public float rotateX = 0;
    public float rotateY = 0;
    public float rotateZ = 0;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private float _decimalX = 0;
    
    private float _decimalZ = 0;
    
    // Update is called once per frame
    void Update()
    {
        if (snapIsTrue)
        {
           
            _decimalX = moveAmount.x % gridSize;
            if ((transform.position.x % gridSize) != 0)
            {
                _decimalX = transform.position.x - (transform.position.x % gridSize);
                //_decimalX = 0;
            }
            else if (_decimalX > gridSize / 2)
            {
                _decimalX = (transform.position.x + moveAmount.x) - _decimalX;
                moveAmount.x = 0;
                _decimalX += gridSize;
            }
            else if (_decimalX < (-gridSize / 2))
            {
                _decimalX = (transform.position.x + moveAmount.x) - _decimalX;
                moveAmount.x = 0;
                _decimalX -= gridSize;
            }
            else
            {
                _decimalX = transform.position.x;
            }

            

            


            // Since we use vector 2, the y in this case represent the z-axis
            _decimalZ = moveAmount.y % gridSize;
            if ((transform.position.z % gridSize) != 0)
            {
                _decimalZ = transform.position.z - (transform.position.z % gridSize);
                //_decimalX = 0;
            }
            else if (_decimalZ > gridSize / 2)
            {
                _decimalZ = (transform.position.z + moveAmount.y) - _decimalZ;
                moveAmount.y = 0;
                _decimalZ += gridSize;
            }
            else if (_decimalZ < (-gridSize / 2))
            {
                _decimalZ = (transform.position.z + moveAmount.y) - _decimalZ;
                moveAmount.y = 0;
                _decimalZ -= gridSize;
            }
            else
            {
                _decimalZ = transform.position.z;
            }

            transform.position = new Vector3(_decimalX, transform.position.y, _decimalZ);

            rotationsnap();

        }
    }

    public void rotationsnap()
    {
        Quaternion currentRotation = transform.rotation;
        if(currentRotation.x >= angelTrigger)
        {
            Debug.Log("yes");
            rotateX += angelTrigger;
            currentXAngel = 0;
        }
        else if(currentRotation.x <= -angelTrigger)
        {
            Debug.Log("no");
            rotateX -= angelTrigger;
            currentXAngel = 0;
        }

        if (currentRotation.y >= angelTrigger)
        {
            rotateY += angelTrigger;
            currentYAngel = 0;
        }
        else if (currentRotation.y <= -angelTrigger)
        {
            rotateY -= angelTrigger;
            currentYAngel = 0;
        }

        if (currentRotation.z >= angelTrigger)
        {
            rotateZ += angelTrigger;
            currentZAngel = 0;
        }
        else if (currentRotation.z <= -angelTrigger)
        {
            rotateZ -= angelTrigger;
            currentZAngel = 0;
        }
        
        Vector3 rotationVector = new Vector3(rotateX, rotateY, rotateZ);
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void OnDrawGizmos()
    {

    }
}
