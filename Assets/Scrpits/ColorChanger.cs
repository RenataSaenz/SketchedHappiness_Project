using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;
    [Header("Materials")]
    [SerializeField]
    private Material _changedMaterial;
    [SerializeField]
    private Material _startingMaterial;
   
    void Start()
    {
        _object.GetComponent<MeshRenderer>().material = _startingMaterial;
        EventManager.Subscribe("Level2Won", ChangeMaterial);
    }

    void ChangeMaterial(params object[] parameters)
    {
        _object.GetComponent<MeshRenderer>().material = _changedMaterial;
    }


}
