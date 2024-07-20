using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public override void Enter(Player player)
    {
        player.ChangeStateText("PlayerAttackState");
    }

    public override void Execute(Player player)
    {
        GameObject enemy = player.GetEnemy();

        if (enemy == null || player.PlayerInMovement())
        {
            player.ChangeState(new PlayerIdleState());
        }
        else if (player.PlayerCarryTower())
        {
            player.ChangeState(new PlayerMoveTowerState());
        }
        else if (player.attackTime >= player.attackInterval)
        {
            enemy.GetComponent<Enemy>().TakeDamage(player.damage);
            player.attackTime = 0;
        }

        //Direct player based on enemy position
        if (enemy != null)
        {
            if (PlayerFacingEnemy(player.transform, enemy.transform))
            {
                player.FaceRight();
            }
            else
            {
                player.FaceLeft();
            }
        }
    }

    bool PlayerFacingEnemy(Transform player, Transform enemy)
    {
        return (player.position.x > enemy.position.x);
    }

    public override void Exit(Player player)
    {
        
    }
 
}
