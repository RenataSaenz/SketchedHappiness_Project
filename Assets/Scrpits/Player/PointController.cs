using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PointController : MonoBehaviour
{
    public Transform point;
    public Material[] mat;

    Vector3 myPos;

    private void Update()
    {
        myPos = point.position;
        foreach (var m in mat)
        {
            m.SetVector("_Point", myPos);
        }
        
    }
}
