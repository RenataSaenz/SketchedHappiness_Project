using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterToRotateOnce : MonoBehaviour
{
    [SerializeField]
    private GameObject _roomAboutToRotate;
    Room _roomAboutToRotateScript;

    private void Start()
    {
        _roomAboutToRotateScript = _roomAboutToRotate.GetComponent<Room>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Feet")
            _roomAboutToRotateScript.counterToRotateOnce = 1;

    }
}
