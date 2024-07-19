using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    bool isPlayerContact = false;
    bool isTurretContact = false;

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

    public override bool IsFacingPlayer()
    {
        return (transform.position.x > player.position.x);
    }

    public override bool IsPlayerNearby()
    {
        return isPlayerContact;
    }

    public override bool IsTurretNearby()
    {
        return isTurretContact;
    }

    public override void MoveTowardsGoal()
    {
        Debug.Log("Move enemy");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COllision with " + gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerContact = true;
        }
        if (collision.gameObject.CompareTag("Tower"))
        {
            isTurretContact = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerContact = false;
        }
        if (collision.gameObject.CompareTag("Tower"))
        {
            isTurretContact = false;
        }
    }
}
