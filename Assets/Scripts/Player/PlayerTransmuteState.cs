using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransmuteState : PlayerState
{
    public override void Enter(Player player)
    {
        player.ChangeStateText("PlayerTransmuteState");
    }

    public override void Execute(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit(Player player)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
