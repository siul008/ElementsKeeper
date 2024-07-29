using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTowerState : PlayerState
{
    public override void Enter(Player player)
    {
    }

    public override void Execute(Player player)
    {
        if (!player.PlayerCarryTower())
        {
            player.ChangeState(new PlayerIdleState());
        }
    }

    public override void Exit(Player player)
    {
        
    }
}
