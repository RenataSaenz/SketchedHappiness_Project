using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public bool warnEnemies = false;
    public Node node;

    public static BeeManager instance;
    
    public List<Bee> allEnemies = new List<Bee>();
    public List<Node> allNodes = new List<Node>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
    public void AddEnemy(Bee e)
    {
        if (!allEnemies.Contains(e))
            allEnemies.Add(e);
    }
    public void AddNode(Node n)
    {
        if (!allNodes.Contains(n))
            allNodes.Add(n);
    }
}
