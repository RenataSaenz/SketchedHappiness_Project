using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntersPassCollider : MonoBehaviour
{
    public bool entersDoor = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            entersDoor = true;
        }
        
    }
}
