using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollows : MonoBehaviour
{
    private GameObject platformStep;
    private GameObject player;
    [SerializeField]
    private bool _playerInPlatform = false;
    private bool _platformToyQuest = false;
    [SerializeField]
    List <Transform> waypoints;
    [SerializeField]
    float moveSpeed;
    public int waypointIndex = 0;
    [SerializeField]
    private float _timeBeforeMoving = 3;
   public  int count = 0;

    bool _stop = false;

    void Start()
    {
        EventManager.Subscribe("ToyQuest", ActivatePlatformToyQuest);
        player = GameObject.FindGameObjectWithTag("Player");
            platformStep = GameObject.FindGameObjectWithTag("PlatformChild");
    }
    
    void ActivatePlatformToyQuest(params object[] parameters)
    {

        _platformToyQuest = true;
        EventManager.UnSubscribe("ToyQuest", ActivatePlatformToyQuest);
    }


    private void FixedUpdate()
    {
        if (_playerInPlatform)
        {
            Move();
        }
        
    }
    private void Move()
    {
        if (!_stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

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
        
        /*
        if (waypointIndex == waypoints.Length-1)
        {
            _comeBack = true;
            if (count == 1)
            {
                _playerInPlatform = false;
                count -= 1;
            }

            //waypointIndex -=1;
        }
        
       // if (_comeBack == true)
        //{
          // waypointIndex -= 1;
       // }
      //  else
        {
         //   waypointIndex += 1;
        }
        if (_playerInPlatform == true)
        {
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                // waypointIndex += 1;

                if (_comeBack == false)
                {
                    waypointIndex += 1;
                    //waypointIndex -= 1;
                }
                else if (_comeBack == true)
                {
                    waypointIndex = 2;
                    if (waypointIndex == 2)
                    {
                        waypointIndex = 0;
                        if (waypointIndex == 0)
                        {
                            _comeBack = false;
                        }
                    }
                }
            }

            StartCoroutine(MoveAfterTime());
        }
      
       

        if (waypointIndex ==0)
        {
            count = 1;
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && _platformToyQuest == true)
        {
            EventManager.Trigger("EntersPlatform");
            player.transform.parent = platformStep.transform;
            _playerInPlatform = true;
                //Physics2D.IgnoreLayerCollision(8, 20, true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.parent = null;
            _playerInPlatform = false;
            //Physics2D.IgnoreLayerCollision(8, 20, false);
        }
    }
    private IEnumerator MoveAfterTime()
    {
        _stop = true;
        yield return new WaitForSeconds(_timeBeforeMoving);
        _stop =false;
    }
}
