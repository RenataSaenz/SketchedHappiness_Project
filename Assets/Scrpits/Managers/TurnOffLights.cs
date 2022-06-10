using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : MonoBehaviour
{
    public List<GameObject> _lights;
    private void Start()
    {
        foreach (var obj in _lights)
            obj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.PLAYER)
        {
            foreach (var obj in _lights)
                obj.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.PLAYER)
        {
            foreach (var obj in _lights)
                obj.SetActive(false);
        }
    }
}
