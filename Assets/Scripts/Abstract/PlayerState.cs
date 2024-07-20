using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void Enter(Player player);
    public abstract void Execute(Player player);
    public abstract void Exit(Player player);
}
