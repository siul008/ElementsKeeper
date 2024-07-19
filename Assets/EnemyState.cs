using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract void Enter(Enemy enemy);
    public abstract void Execute(Enemy enemy);
    public abstract void Exit(Enemy enemy);
}
