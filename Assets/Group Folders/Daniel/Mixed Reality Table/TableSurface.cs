using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Surfaces;

public class TableSurface : MonoBehaviour
{
    [SerializeField]
    private PlaneSurface _planeSurface;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(Vector3.up * 10, new Vector3(10, 0.1f, 10));
    }
}
