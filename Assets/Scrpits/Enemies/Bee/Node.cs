using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    public List<Node> neighbours;
    public int cost = 1;
    [SerializeField]private float viewRadius = 100;
    
    [Header("Layers")]
    private LayerMask obstacleMask;
    private LayerMask nodesMask;

    private event Action OnAddingNodes;
    #endregion
    private void Awake()
    {
        neighbours.Clear();
        BeeManager.instance.AddNode(this);
        obstacleMask = LayerMask.GetMask("Walls");
        nodesMask = LayerMask.GetMask("Nodes");

        OnAddingNodes += FieldOfView;
        OnAddingNodes?.Invoke();
    }

    public void FieldOfView()
    {
        Collider[] nodesInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, nodesMask);
        
        foreach (var n in nodesInViewRadius)
        {
            var _node = n.GetComponent<Node>();
            
            if (_node != null && InSight(transform.position,n.gameObject.transform.position) && !neighbours.Contains(_node) && _node !=this)
            {
                neighbours.Add(_node);
            }
        }
    }
    
    public bool InSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        if (!Physics.Raycast(start, dir, dir.magnitude, obstacleMask))return true;
        else return false;
    }
}