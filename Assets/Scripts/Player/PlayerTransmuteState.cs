using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransmuteState : PlayerState
{
    public override void Enter(Player player)
    {
        CraftingManager.Instance.UpdateFragmentText();
        SoundManager.Instance.InstantPlaySfx("TransmuteBook", false);
        player.SetCraftingTableUIVisibility(true);
        player.PauseGame();
    }

    public override void Execute(Player player)
    {
        if (!player.PlayerNearTransmute())
        {
            player.ChangeState(new PlayerIdleState());
        }
    }

    public override void Exit(Player player)
    {
        player.UnpauseGame();
        player.SetCraftingTableUIVisibility(false);
    }
}
