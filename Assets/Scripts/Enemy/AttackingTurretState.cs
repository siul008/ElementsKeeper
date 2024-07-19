using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTurretState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        Debug.Log("Entering Turret Attack State for " + enemy.gameObject.name);
    }

    public override void Execute(Enemy enemy)
    {
       if (!enemy.IsTurretNearby())
       {
            enemy.ChangeState(new WalkingState());
       }
       else
       {
           Debug.Log("Attacking Tower");
       }
    }

    public override void Exit(Enemy enemy)
    {
        Debug.Log("Entering Turret Attack State for " + enemy.gameObject.name);
    }
}
