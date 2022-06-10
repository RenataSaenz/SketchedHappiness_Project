using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyObject : MonoBehaviour
{
    [SerializeField]
    private int _points;

    public void OnTriggerStay(Collider trig)
    {
        var damageable = trig.transform.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.AddLifeFunc(_points);
        }
        
    }
}
