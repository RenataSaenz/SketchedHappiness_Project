using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private Transform player;
    [Header("Mom")]
    [SerializeField]
    private GameObject mom;
    [Header("Materials")]
    [SerializeField]
    private Material Material1;
    [SerializeField]
    private Material Material2;

    [SerializeField] private Chair[] _chair;
    //[SerializeField] private Table _table;
    
    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        foreach (var c in _chair)
        {
            
        }


        //if (_chair.underChair) CheckChair();

        //if (_table.underTable == false) Pursuit();
        //if (_table.underTable)


    }

    void CheckChair()
    {
        
    }

    void Pursuit()
    {
        
    }

    public void Start()
    {
       // Interactive.instance.FirstMom += firstDot;
    }
    
    private void Update()
    {
        //transform.LookAt(player);
    }

    void firstDot()
    {
       // transform.position = DotManager.instance.dots[0].dots[0].transform.position;
        GetComponent<MeshRenderer>().material = Material1;
    }
    void secondDot()
    {
        GetComponent<MeshRenderer>().material = Material2;
    }
}
