using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransmuteState : PlayerState
{
    float currentProg;
    public override void Enter(Player player)
    {
        player.ChangeStateText("PlayerTransmuteState");
        player.SetCraftingTableUIVisibility(true);
    }

    public override void Execute(Player player)
    {
        if (!player.PlayerNearTransmute() /*|| player.PlayerInMovement()*/)
        {
            player.ChangeState(new PlayerIdleState());
        }
        /*if (InventoryManager.Instance.GetFragmentsCost() <= InventoryManager.Instance.GetFragments())
        {
            if (currentProg > player.craftingTime)
            {
                InventoryManager.Instance.PurchaseTower();
                currentProg = 0;
            }
            else
            {
                currentProg += Time.deltaTime;
            }
        }
        player.UpdateProgBar(currentProg, player.craftingTime);*/
    }

    public override void Exit(Player player)
    {
        //player.UpdateProgBar(0, player.craftingTime);
        player.SetCraftingTableUIVisibility(false);
    }
}
