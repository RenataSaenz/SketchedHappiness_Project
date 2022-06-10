using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : Enemy
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform _runLeft;
    [SerializeField]
    private Transform _runRight;

    public void Start()
    {
        _target = GameObject.FindWithTag("Feet").transform;
    }
    private void Update()
    {
        Move();

    }
    override public void Move()
    {
        transform.LookAt(_target);

        if (Vector3.Distance(transform.position, _target.position) <= _areaDistance)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            Action();
            
            if (Vector3.Distance(transform.position, _target.position) <= _areaDistance2)
            {
                transform.LookAt(_runLeft);
                transform.position += transform.forward * _speed * Time.deltaTime;
                
            }
        }

    }
    
    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "AttackToLeft")
        {
            transform.LookAt(_target);
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }
    override public void Die()
    {
        base.Die();
        EnemyManager.instance.ReturnMannequin(this);
        AudioManager.instance.Play(AudioManager.Types.Explosion);
    }
}
