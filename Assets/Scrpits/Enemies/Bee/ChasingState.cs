using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : IState
{
    StateMachine _fsm;
    Animator _anim;
    private float _attackRange;
    private Bee _enemy;
    Vector3 targetFuturePos;
    
    public ChasingState(StateMachine fsm, Bee w,float attackRange)
    {
        _attackRange = attackRange;
        _fsm = fsm;
        _enemy = w;
    }

    public void OnStart()
    {
        BeeManager.instance.warnEnemies = true;
        Debug.Log(_enemy.ToString()+ " ENTERS SHOOTING");
    }

    public void OnUpdate()
    {
        if (_enemy.escape == false)
        {
            FieldOfView();
            InAttackRange();
        }
        else if (_enemy.escape)
        {
            _fsm.ChangeState(PlayerStatesEnum.FindPollen);
        }
    }

    public void OnExit()
    {
        BeeManager.instance.warnEnemies = false;
        _enemy.totalDamage = 0;
        _enemy.escape = false;
    }

    public void FieldOfView()
    {
        _enemy.Chase(_enemy.target);
        Debug.DrawLine(_enemy.transform.position, _enemy.target.transform.position, Color.red);
        
        if(!_enemy.InSight(_enemy.transform.position, _enemy.target.transform.position))
        {
            _fsm.ChangeState(PlayerStatesEnum.FindPollen);
            Debug.DrawLine(_enemy.transform.position, _enemy.target.transform.position, Color.green);
        }
    }

    void InAttackRange()
    { 
        Vector3 dist = _enemy.target.transform.position - _enemy.transform.position;
        if (dist.magnitude <= _attackRange)
        {
            _fsm.ChangeState(PlayerStatesEnum.Attack);
        }
    }
    
}