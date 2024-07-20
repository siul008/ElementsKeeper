using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Enter(Player player)
    {
        player.ChangeStateText("PlayerIdleState");
    }

    public override void Execute(Player player)
    {
        if (player.PlayerCarryTower())
        {
            player.ChangeState(new PlayerMoveTowerState());
        }
        else if (!player.PlayerInMovement() && player.GetEnemy() != null)
        {
            player.ChangeState(new PlayerAttackState());
        }
        else if (!player.PlayerInMovement() && player.PlayerNearTransmute())
        {
            player.ChangeState(new PlayerTransmuteState());
        }
    }

    public override void Exit(Player player)
    {

    }
}
