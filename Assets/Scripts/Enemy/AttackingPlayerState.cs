using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayerState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
        Debug.Log("Entering Attacking State for " + enemy.gameObject.name);
    }

    public override void Execute(Enemy enemy)
    {
        if (!enemy.IsPlayerNearby() || !enemy.IsFacingPlayer())
        {
            enemy.ChangeState(new WalkingState());
        }
        else
        {
            Debug.Log("Attacking player");
        }
    }

    public override void Exit(Enemy enemy)
    {
        Debug.Log("Leaving Walking State for " + enemy.gameObject.name);
    }
}
