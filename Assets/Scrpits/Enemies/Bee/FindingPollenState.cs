using System.Collections.Generic;
using UnityEngine;
public class FindingPollenState : IState
{
    StateMachine _fsm;
    private Bee _enemy;
    private Node _goalFlower;
    private float _viewRadius;
    private float _viewAngle;
    private Node _startingNode;

    private List<Node> findList;
    private int nextPoint = 0;
    private LayerMask _detectableAgentMask;
    private GameObject _pollenSpot;
    private List<GameObject> _flowers;

    public FindingPollenState(StateMachine fsm, Bee e, float viewRadius, LayerMask detectableAgentMask, float viewAngle, GameObject pollenSpot, List<GameObject> flowers)
    {
        _flowers = flowers;
        _pollenSpot = pollenSpot;
        _viewAngle = viewAngle;
        _detectableAgentMask = detectableAgentMask;
        _viewRadius = viewRadius;
        _fsm = fsm;
        _enemy = e;
    }

    public void OnStart()
    {
        
        if (_enemy.currentFlower >= _flowers.Count)
            _enemy.currentFlower = 0;
        
        _goalFlower = _flowers[_enemy.currentFlower].GetComponent<Node>();
        
        _startingNode = CheckNearestStart(_enemy.transform.position);
        
       findList = ConstructPathAStar(_startingNode, _goalFlower);
        
    }

    public void OnUpdate()
    {
        FieldOfView();
        _enemy.FindingMove(findList);
        
       if (BeeManager.instance.warnEnemies)
       {
           _enemy.current = 0;
           _fsm.ChangeState(PlayerStatesEnum.Hunt);
       }
       
       if (_enemy.current  >= findList.Count)
       {
           _enemy.current  = 0; 
           var pollen = _goalFlower.transform.GetComponentInChildren<Pollen>();
           if (pollen != null)
           {
               pollen.gameObject.transform.parent = _pollenSpot.transform;
               pollen.transform.position = _pollenSpot.transform.position;
               _fsm.ChangeState(PlayerStatesEnum.FindHoneycomb);
           }
           else
           {
               _enemy.currentFlower ++;
               if (_enemy.currentFlower >= _flowers.Count)
                   _enemy.currentFlower = 0;
               
               _fsm.ChangeState(PlayerStatesEnum.FindHoneycomb);
           }
       }
       
       
    }
    public void OnExit()
    {
    }
    public List<Node> ConstructPathAStar(Node startingNode, Node goalNode)
    {
        if (startingNode == null || goalNode == null)
            return default;

        PriorityQueue frontier = new PriorityQueue();
        frontier.Put(startingNode, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();

        cameFrom.Add(startingNode, null);
        costSoFar.Add(startingNode, 0);

        while (frontier.Count() > 0)
        {
            Node current = frontier.Get();

            if (current == goalNode)
            {
                List<Node> path = new List<Node>();
                Node nodeToAdd = current;

                while (nodeToAdd != null)
                {
                    path.Add(nodeToAdd);
                    nodeToAdd = cameFrom[nodeToAdd];
                }
                path.Reverse();
                return path;
            }

            foreach (var next in current.neighbours)
            {
                int newCost = costSoFar[current] + next.cost;

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    if (costSoFar.ContainsKey(next))
                    {
                        costSoFar[next] = newCost;
                        cameFrom[next] = current;
                    }
                    else
                    {
                        cameFrom.Add(next, current);
                        costSoFar.Add(next, newCost);
                    }

                    float priority = newCost + Heuristic(next.transform.position, goalNode.transform.position);
                    frontier.Put(next, priority);
                }
            }
        }
        return default;
    }

    public float Heuristic(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }

    public Node CheckNearestStart(Vector3 pos)
    {
        PriorityQueue e = new PriorityQueue();
        foreach (var item in _enemy.pathNodes)
        {
            if (_enemy.InSight(_enemy.transform.position, item.transform.position)) e.Put(item, Heuristic(item.transform.position, pos));
        }
        return e.Get();
    }
    
    public void FieldOfView()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(_enemy.transform.position, _viewRadius, _detectableAgentMask);

        foreach (var item in targetsInViewRadius)
        {
            _enemy.target = item.gameObject;
        }

        if (_enemy.target == null) return;
        
        Vector3 dirToTarget = (_enemy.target.transform.position - _enemy.transform.position);

        if (Vector3.Angle(_enemy.transform.forward, dirToTarget.normalized) < _viewAngle / 2)
        {
            Debug.Log("player in angle");
            if (_enemy.InSight(_enemy.transform.position, _enemy.target.transform.position))
            {
                BeeManager.instance.node = CheckNearestStart(_enemy.transform.position);
                _fsm.ChangeState(PlayerStatesEnum.Chase);
            }
        }

    }
}