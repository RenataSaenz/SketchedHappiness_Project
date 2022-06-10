using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMagic : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private float _timeBeforeDestroying;
   // ManagerMood _managerMood;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(Layers.ENEMY, Layers.ENEMYDETECTOR);
        //_managerMood = GameObject.Find("ManagerMood").GetComponent<ManagerMood>();
    }
    private void Start()
    {
        _rb.velocity = transform.forward * _speed;
        if (_timeBeforeDestroying > 0f)
        {
            StartCoroutine(DestroyAfterTime());
        }
    }

    private void OnTriggerEnter(Collider trig)
    {
        var damageable = trig.transform.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.SubtractLifeFunc(5);
        }
        /*if (trig.gameObject.layer == Layers.PLAYER)
        {
            _managerMood.SubtractPoints(5);
            Debug.Log(_managerMood.ReturnPoints());
        }*/
        Destroy(this.gameObject);
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeDestroying);

        Destroy(this.gameObject);
    }

    
}
