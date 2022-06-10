using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCloseDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject _door;
    private int _timeBeforeClosing = 1;
    Door _doorScript;

    private void Start()
    {
        _doorScript = _door.GetComponent<Door>();
    }
    private void Update()
    {
        if (_doorScript.isOpen == true)
        {
            StartCoroutine(CloseDoor());
        }
    }
    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(_timeBeforeClosing);
        _doorScript.isOpen=false;
    }
}
