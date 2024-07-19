using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
    }

    public override void Execute(Enemy enemy)
    {
        if (enemy.IsPlayerNearby() && enemy.IsFacingPlayer())
        {
            enemy.ChangeState(new AttackingPlayerState());
        }
        else if (enemy.IsTurretNearby())
        {
            enemy.ChangeState(new AttackingTurretState());
        }
        else
        {
            enemy.MoveTowardsGoal();
        }
    }

    public override void Exit(Enemy enemy)
    {
    }
}
