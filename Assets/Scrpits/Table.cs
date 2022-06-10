using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool underTable;
    public void OnTriggerStay(Collider trig)
    {
        if (trig.gameObject.layer == Layers.PLAYER)
        {
            underTable = true;
        }
    }
}
