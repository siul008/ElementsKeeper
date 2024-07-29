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
        SoundManager.Instance.InstantPlaySfx("EnemyHit", true);
        Instantiate(voidFragment, transform.position, Quaternion.identity);
        SpawnerScript.Instance.EnemyDied();
        Instantiate(particles, transform.position, Quaternion.identity);
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
        else
        {
            StartCoroutine(SoundManager.Instance.PlaySfx("EnemyHit", Random.Range(0, 0.15f)));
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
