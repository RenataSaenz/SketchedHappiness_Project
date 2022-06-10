using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MovingDoor : MonoBehaviour, ICollectable
{
    #region Variables
    [SerializeField]private Transform _player;
    
    [Header("Stats")]
    private Vector3 _velocity;
    [SerializeField]private float maxSpeed;
    [SerializeField]private float maxForce;
    [SerializeField]private float evadeRadius;
    [SerializeField]private LayerMask enemyMask;
    private GameObject _enemy;
    [SerializeField] private float _timeBeforeStopping = 2;
    
    [Header("WayPoints")]
    public List<Transform> wayPoints = new List<Transform>();
    private int _wayPointIndex = 0;
    
    [Header("Balance")]
    public float separationWeight;
    public float wayPointsWeight;

    [Header("Objects")]
    [SerializeField] private GameObject DoorRoom;
    [SerializeField] private ParticleSystem _particleSystem;
    
    public event Action OnOpen;
    private bool _enableOpen = false;
    #endregion
    
    void Start()
    {
        Debug.Log(_enableOpen.ToString());
        OnOpen += PlaygroundManager.instance.EndLevelDoor;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        randomDirection.Normalize();
        randomDirection *= maxSpeed;

        ApplyForce(randomDirection);
    }

    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;

        ApplyForce(Separation() * separationWeight + WayPoints() * wayPointsWeight);
        CheckBounds();
        transform.LookAt((_player));
        CheckEnemies();
    }

    void CheckEnemies()
    {
        if (EnemyManager.instance.totalEnemiesCounter == EnemyManager.instance.counter)
        {
            StartCoroutine(StopAfterTime());
        }
    }

    private IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeStopping);
        _enableOpen = true;
        _player = null;
        maxSpeed = 0;
        EnemyManager.instance.ActiveEnemy = delegate { };
    }
    
    public void Collect()
    {
        if (_enableOpen)
        { 
            Debug.Log("collect");
            Debug.Log(_enableOpen.ToString());
            AudioManager.instance.Play(AudioManager.Types.Door);
            Instantiate(_particleSystem, transform.position, transform.rotation);
            OnOpen?.Invoke();
            DoorRoom.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, evadeRadius);
    }

    Vector3 Separation()
    {
        Vector3 desired = new Vector3();
        int nearbyBoids = 0;
        
        
        Collider[] enemiesInViewRadius = Physics.OverlapSphere(transform.position, evadeRadius, enemyMask);

        foreach (var item in enemiesInViewRadius)
        {
            Vector3 dist = item.transform.position - transform.position;
             //  desired += dist;
                desired.x += dist.x;
                desired.z += dist.z;
                nearbyBoids++;
            
        }
        if (nearbyBoids == 0) return desired;
        desired /= nearbyBoids;
        desired.Normalize();
        desired *= maxSpeed;
        desired = -desired;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }

    Vector3 WayPoints()
    { 
        Vector3 desired = wayPoints[_wayPointIndex].transform.position - transform.position;
        
        if (desired.magnitude < 0.15f)
        {
            _wayPointIndex++;
        }

        if (_wayPointIndex > wayPoints.Count-1)
            _wayPointIndex = 0;
        
        desired.Normalize();
        desired *= maxSpeed;

        Vector3 steering = Vector3.ClampMagnitude(desired - _velocity, maxForce);

        return steering;
    }
    
    void CheckBounds()
    {
        if (transform.position.z < -163.23) transform.position = new Vector3(transform.position.x, transform.position.y, -109.6f);
        if (transform.position.z > -109.6) transform.position = new Vector3(transform.position.x, transform.position.y, -163.23f);
        if (transform.position.x > -131.1) transform.position = new Vector3(-179.1f, transform.position.y, transform.position.z);
        if (transform.position.x < -179.1) transform.position = new Vector3(-131.1f, transform.position.y, transform.position.z);
    }
    
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);
    }
}
