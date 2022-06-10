using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    List<Transform> waypoints;
    public int waypointIndex = 0;
    [SerializeField]
    private float _timeBeforeMoving = 4;
    bool _stop = false;
    private void Update()
    {
        Move();

    }

    override public void Move()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _areaDistance)
        {
            transform.LookAt(_target);

            transform.position += transform.forward * _speed * Time.deltaTime;
            Action();
            /*
            if (Vector3.Distance(transform.position, _target.position) <= _areaDistance2)
            {
                transform.LookAt(_runLeft);
                transform.position += transform.forward * _speed * Time.deltaTime;

            }*/
        }
        else
        {
            if (!_stop)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, _speed * Time.deltaTime);

                if (Vector3.Distance(waypoints[waypointIndex].transform.position, transform.position) < 0.1f)
                {
                    waypointIndex++;
                    if (waypointIndex > waypoints.Count - 1)
                    {
                        waypointIndex = 0;
                        waypoints.Reverse();
                        StartCoroutine(MoveAfterTime());
                    }
                }
            }
               
        }

    }
    private IEnumerator MoveAfterTime()
    {
        _stop = true;
        yield return new WaitForSeconds(_timeBeforeMoving);
        _stop = false;
    }
}
