using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : IState
{
    StateMachine _fsm;
    Animator _anim;
    private Bee _enemy;
    Vector3 targetFuturePos;
    float timer;
    private Animator _animator;
    private float _attackRange;
    public AttackingState(StateMachine fsm, Bee w, Animator animator, float attackRange)
    {
        _fsm = fsm;
        _enemy = w;
        _animator = animator;
        _attackRange = attackRange;
    }

    public void OnStart()
    {
        Attack();
        timer = _enemy.fireRate;
    }

    public void OnUpdate()
    {
        if (_enemy.escape == false)
        {
            Await();
            InAttackRange();
        }
        else if (_enemy.escape)
        {
            _fsm.ChangeState(PlayerStatesEnum.FindPollen);
        }
    }

    public void OnExit()
    {
       timer = _enemy.fireRate;
       _enemy.totalDamage = 0;
       _enemy.escape = false;
    }
    
    public void Await()
    {
        timer-=Time.deltaTime;
        
        if (timer <= 0)
        {
            Attack();
            _fsm.ChangeState(PlayerStatesEnum.Chase);
        }
        else
        {
            _animator.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {
        timer = _enemy.fireRate;
        _animator.SetBool("isAttacking", true);
    }
    
    void InAttackRange()
    { 
        Vector3 dist = _enemy.target.transform.position - _enemy.transform.position;
        if (dist.magnitude >= _attackRange)
        {
            _fsm.ChangeState(PlayerStatesEnum.Chase);
        }
    }
}