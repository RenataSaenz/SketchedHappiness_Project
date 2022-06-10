using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool underChair = false;
    public GameObject _soul;
    // [SerializeField]private Vector3 _center;
    // [SerializeField]private Vector3 _size;
    // bool m_HitDetect;
    // RaycastHit m_Hit;
    // [SerializeField]private float m_MaxDistance = 0;

    private void FixedUpdate()
    {
       // FieldOfView();
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.layer == Layers.SOUL)
        {
            underChair = true;
        }
    }
    private void OnTriggerExit(Collider trig)
    {
        if (trig.gameObject.layer == Layers.SOUL)
        {
            underChair = false;
        }
    }

    //
    // public void FieldOfView()
    // {
    //     
    //     //Test to see if there is a hit using a BoxCast
    //     //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
    //     //Also fetch the hit data
    //
    //     // m_HitDetect = Physics.BoxCast(transform.position + _center, _size*2, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
    //     //  if (m_HitDetect)
    //     //  {
    //     //      //Output the name of the Collider your Box hit
    //     //      Debug.Log("Hit : " + m_Hit.collider.name);
    //     //  }
    //      
    //  Collider[] targetsInArea = Physics.OverlapBox(transform.position + _center, _size*2,Quaternion.identity, Layers.SOUL);
    //  
    //  foreach (var item in targetsInArea)
    //  {
    //      _soul = item.gameObject;
    //  }
    //
    //  InSight(transform.position + _center, _soul.transform.position);
    //  int i = 0;
    //  if (i < targetsInArea.Length)
    //  {
    //      //Output all of the collider names
    //      Debug.Log("Hit : " + targetsInArea[i].name + i);
    //      //Increase the number of Colliders in the array
    //      i++;
    //      underChair = true;
    //  }
    //
    //  if (i > targetsInArea.Length)
    //  {
    //      i--;
    //      underChair = false;
    //  }
    //  
    //
    //  // Vector3 dirToTarget = (_enemy.target.transform.position - _enemy.transform.position);
    //  //
    //  // if (Vector3.Angle(_enemy.transform.forward, dirToTarget.normalized) < _viewAngle / 2)
    //  // {
    //  //     Debug.Log("player in angle");
    //  //     if (_enemy.InSight(_enemy.transform.position, _enemy.target.transform.position))
    //  //     {
    //  //         BeeManager.instance.node = CheckNearestStart(_enemy.transform.position);
    //  //         _fsm.ChangeState(PlayerStatesEnum.Chase);
    //  //     }
    //  // }
    //
    //
    // }
    // public bool InSight(Vector3 start, Vector3 end)
    // {
    //     Vector3 dir = end - start;
    //     if (Physics.Raycast(start, dir, dir.magnitude, Layers.SOUL))return true;
    //     else return false;
    // }
    
    
    
    
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(transform.position + _center, _size);
    // }
    //
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //
    //     //Check if there has been a hit yet
    //     if (m_HitDetect)
    //     {
    //         //Draw a Ray forward from GameObject toward the hit
    //         Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
    //         //Draw a cube that extends to where the hit exists
    //         Gizmos.DrawWireCube(transform.position + _center + transform.forward * m_Hit.distance, _size);
    //     }
    //     //If there hasn't been a hit yet, draw the ray at the maximum distance
    //     else
    //     {
    //         //Draw a Ray forward from GameObject toward the maximum distance
    //         Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
    //         //Draw a cube at the maximum distance
    //         Gizmos.DrawWireCube(transform.position + _center + transform.forward * m_MaxDistance, _size);
    //     }
    // }
}
