using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingRoom : MonoBehaviour
{
    [SerializeField]private GameObject _mum;
    void OnCollisionEnter (Collision with)
    {
        if (with.gameObject.layer == Layers.PLAYER)
        {
        }
    }
}
