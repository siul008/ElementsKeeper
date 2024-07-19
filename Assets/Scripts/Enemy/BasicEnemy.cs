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
        return false;
    }

    public override bool IsTurretNearby()
    {
        return false;
    }

    public override void MoveTowardsGoal()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
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
