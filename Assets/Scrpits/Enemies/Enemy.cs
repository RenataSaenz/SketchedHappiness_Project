using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    protected float _speed = 4f;
    [SerializeField]
    protected float _health = 1f;
    [SerializeField]
    protected float _areaDistance3 = 0;
    [SerializeField]
    protected float _areaDistance2 = 0;
    [SerializeField]
    protected float _areaDistance = 30;
    [SerializeField]
    protected Transform _target;
    [SerializeField]
    protected GameObject _sound;
    [SerializeField]
    protected ParticleSystem _particleSystem;
    [SerializeField]
    protected int _damageNear;

    public float range = 1f;

    private EnemyManager _manager;


    public void Awake()
    {
        _target = Camera.main.transform;
        if (_sound != null)
            _sound.SetActive(false);
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, range))
        {
            var damageable = hit.transform.gameObject.GetComponent<IDamageable>();
            if (damageable != null )
            {
                damageable.SubtractLifeFunc(_damageNear);
                Die();
            }
        }
    }
 
    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0f)
        {  
            Die();
        }
    }

    virtual public void Die()
    {
        Instantiate(_particleSystem, transform.position, transform.rotation);
    }

    virtual public void Move()
    {
        transform.LookAt(_target);
     
        if(Vector3.Distance(transform.position, _target.position) <= _areaDistance)
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            Action();
        }
        if (Vector3.Distance(transform.position, _target.position) <= _areaDistance2)
        {
            //Posibilidad llamar alguna funcion
        }
    }
    
    virtual public void Action()
    {
        if (_sound != null)
            _sound.SetActive(true);
    }
    public static void TurnOff(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public static void TurnOn(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    
    public void  SpawnEnemy(EnemyManager manager, GameObject floorParent, Vector3 pos)
    {
        _manager = manager;
        transform.SetParent(floorParent.transform, false);
        transform.position = pos;
    }
   
}
