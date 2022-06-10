using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : Enemy
{ 
    private void Update()
    {
       Move();
    }
    
    override public void Die()
    {
        base.Die();
        EnemyManager.instance.ReturnGhost(this);
    }
   
}
