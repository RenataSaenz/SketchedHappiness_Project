using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{ 
    [SerializeField]
    protected GameObject _room;

    [System.NonSerialized]
    public int counter;
    [System.NonSerialized]
    public int degrees;

    public virtual void Start()
    {
        counter = 0;
    }
    
}
