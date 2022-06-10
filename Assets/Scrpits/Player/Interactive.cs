using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactive : MonoBehaviour
{
    public void OnTriggerStay(Collider trig)
    {
        // var damageable = trig.transform.gameObject.GetComponent<IDamageable>();
        //
        // if (damageable != null)
        // {
        //     damageable.SubtractLifeFunc(5);
        // }
        // if ((Input.GetKeyDown(KeyCode.E)&& damageable != null))
        // {
        //     switch (trig.gameObject.layer)
        //     {
        //         case 9:
        //             //happy
        //             damageable.AddLifeFunc(1);
        //             break;
        //         case 10:
        //             //sad
        //             damageable.SubtractLifeFunc(1);
        //             break;
        //         case 11:
        //             //melancholy
        //             damageable.AddLifeFunc(2);
        //             damageable.SubtractLifeFunc(0.5f);
        //             break;
        //     }
        // }
        
        var collectable = trig.transform.gameObject.GetComponent<ICollectable>();
        
        if ((Input.GetKeyDown(KeyCode.E) && collectable !=null)) collectable.Collect();
    }
}
