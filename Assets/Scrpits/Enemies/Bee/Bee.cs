using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bee : MonoBehaviour
{
    #region Variables
    [Header("Stats")]
    private Vector3 _velocity;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxForce;
    public float fireRate;

    [Header("Field of View")]
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _detectableAgentMask;
    
    [Header("Area")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _separationDistance;
    
    [Header("Spots")]
    [SerializeField] private List<GameObject> _flowers;
    [SerializeField] private Node _honeyComb;
    [SerializeField] private GameObject _pollenSpot;
    
    [Header("Systems")]
    [SerializeField] private Animator _animator;
    // [SerializeField] private ParticleSystem _damageTakenParticles;
    // [SerializeField] private ParticleSystem _attackParticles;
    
    [NonSerialized]public int current = 0;
    [NonSerialized] public int currentFlower = 0;
    [NonSerialized] public GameObject target;
    [NonSerialized]public float totalDamage;
    [NonSerialized]public bool escape = false;
    private StateMachine _fsm;
    [NonSerialized]public List<Node> pathNodes;
    #endregion
    
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        BeeManager.instance.AddEnemy(this);
        pathNodes = BeeManager.instance.allNodes;
        _fsm = new StateMachine();
        _fsm.AddState(PlayerStatesEnum.Chase, new ChasingState(_fsm, this, _attackRange));
        _fsm.AddState(PlayerStatesEnum.Hunt, new HuntingState(_fsm, this, _viewRadius, _viewAngle, _detectableAgentMask));
        _fsm.AddState(PlayerStatesEnum.FindPollen, new FindingPollenState(_fsm, this,_viewRadius, _detectableAgentMask, _viewAngle,_pollenSpot,_flowers));
        _fsm.AddState(PlayerStatesEnum.FindHoneycomb, new FindingHoneycombState(_fsm, this, _honeyComb, _viewRadius, _detectableAgentMask, _viewAngle));
        _fsm.AddState(PlayerStatesEnum.Attack, new AttackingState(_fsm, this, _animator, _attackRange));
        _fsm.ChangeState(PlayerStatesEnum.FindPollen);
    }
    
    void FixedUpdate()
    {
        _fsm.OnUpdate();
        ApplyForce(Separation());
    }
    public void Chase(GameObject _target)
    {
        Vector3 dist = _target.transform.position - transform.position;
        
        Vector3 desired = dist;
        desired.y = 0;
        desired.Normalize();
        desired *= _maxSpeed;
        
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        
        
        ApplyForce(steering);
        Movement();
    }

    void Movement()
    { 
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;
    }
    
    Vector3 Separation()
    {
        Vector3 desired = new Vector3();
        int nearbyBoids = 0;

        foreach(var boid in BeeManager.instance.allEnemies)
        {
            Vector3 dist = boid.transform.position - transform.position;

            if(boid != this && dist.magnitude < _separationDistance)
            {
                //desired += dist;
                desired.x += dist.x;
                desired.z += dist.z;
                nearbyBoids++;
            }
        }

        if (nearbyBoids == 0) return desired;
        desired /= nearbyBoids;
        desired.Normalize();
        desired *= _maxSpeed;
        desired = -desired;
        transform.LookAt(target.transform);

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);
        
        //ApplyForce(steering);
        return steering;
    }
    
    public void FindingMove(List<Node> path)
    {
        if (path == null ||path.Count == 0) return;
        
        Vector3 desired = path[current].transform.position - transform.position;
        
        if (desired.magnitude < 0.15f)
        {
            current++;
        }
        
        desired.Normalize();
        desired *= _maxSpeed;

        Vector3 steering = Vector3.ClampMagnitude(desired - _velocity, _maxForce);

        ApplyForce(steering);
        Movement();
    }
    
    public bool InSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        if (!Physics.Raycast(start, dir, dir.magnitude, _obstacleMask))return true;
        else return false;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);

        Vector3 lineA = DirFromAngle(_viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = DirFromAngle(-_viewAngle / 2 + transform.eulerAngles.y);

        Gizmos.DrawLine(transform.position, transform.position + lineA * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * _viewRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    
    }
    public void TakeDamage(float dmg)
    {
        totalDamage += dmg;
        if (totalDamage >= 1)
        {
            escape = true;
        }
    }
    Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
    }

    public void MakeDamage()
    {
        Vector3 dist = target.transform.position - transform.position;
        if (InSight(transform.position, target.transform.position) && dist.magnitude <= _attackRange)
            target.GetComponent<IDamageable>().SubtractLifeFunc(10);
    }

} 