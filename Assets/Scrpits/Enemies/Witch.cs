using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : Enemy
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float _timeBeforeDisappearing;
    private float _resetTimeBeforeDisappearing;
    [SerializeField]
    private GameObject magicPrefab;
    [SerializeField]
    private Transform shootingPoint;
    private void Start()
    {
        _resetTimeBeforeDisappearing = _timeBeforeDisappearing;
        AudioManager.instance.Play(AudioManager.Types.WitchCast);
        Instantiate(_particleSystem, transform.position, transform.rotation);
        //transform.LookAt(_target);
        
        if (_timeBeforeDisappearing > 0f)
        {
            StartCoroutine(DestroyAfterTime());
        }
    }
    private void Update()
    {
        Move();
    }
    override public void Move()
    {
        transform.LookAt(_target);
        Action();
    }
    override public void Action()
    {
        base.Action();
    }
    
    override public void Die()
    {
        base.Die();
        _timeBeforeDisappearing = _resetTimeBeforeDisappearing;
        EnemyManager.instance.ReturnWitch(this);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeDisappearing);
        AudioManager.instance.Play(AudioManager.Types.WitchCast);
        Die();
    }

    public void Attack()
    {
        GameObject createdProjectile = Instantiate(magicPrefab, shootingPoint.position, shootingPoint.rotation);
        createdProjectile.transform.LookAt(_target.transform);
    }

}
