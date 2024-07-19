using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public override void AttackPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackTurret()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override bool IsPlayerNearby()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsTurretNearby()
    {
        throw new System.NotImplementedException();
    }

    public override void MoveTowardsGoal()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
        UpdateHealthBar();
    }
}
