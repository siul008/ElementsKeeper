using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    bool isPlayerContact = false;

    public override void AttackTurret()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        int drop = Random.Range(0, 101);
        if (drop <= dropChance)
        {
            Instantiate(voidFragment, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public override bool IsFacingPlayer()
    {
        return (transform.position.x > player.transform.position.x);
    }

    public override bool IsPlayerNearby()
    {
        return isPlayerContact;
    }

    public override bool IsTurretNearby()
    {
        return turret != null;
    }

    public override void MoveTowardsGoal()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerContact = true;
        }
        if (collision.gameObject.CompareTag("Tower"))
        {
            turret = collision.gameObject;
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
            turret = null;
        }
    }
}
