using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingPlayerState : EnemyState
{
    public override void Enter(Enemy enemy)
    {
    }

    public override void Execute(Enemy enemy)
    {
        if (!enemy.IsPlayerNearby() || !enemy.IsFacingPlayer())
        {
            if (!enemy.IsTurretNearby())
            {
                enemy.ChangeState(new WalkingState());
            }
            else
            {
                enemy.ChangeState(new AttackingTurretState());
            }
        }
        else
        {
            if (enemy.attackTime >= enemy.attackInterval)
            {
                enemy.player.GetComponent<Player>().TakeDamage(enemy.damage);
                enemy.attackTime = 0;
            }
        }
    }

    public override void Exit(Enemy enemy)
    {
    }
}
