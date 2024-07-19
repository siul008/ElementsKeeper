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
            if (enemy.IsPlayerNearby() && enemy.IsFacingPlayer())
            {
                enemy.ChangeState(new AttackingPlayerState());
            }
            else
            {
                enemy.ChangeState(new WalkingState());
            }
        }
        else
        {
            if (enemy.attackTime >= enemy.attackInterval)
            {
                enemy.turret.GetComponent<TowerScript>().TakeDamage(enemy.damage);
                enemy.attackTime = 0;
            }
        }
    }

    public override void Exit(Enemy enemy)
    {
        Debug.Log("Entering Turret Attack State for " + enemy.gameObject.name);
    }
}
