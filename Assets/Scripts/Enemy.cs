using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float currentHealth;
    Slider healthBar;

    protected EnemyState currentState;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Execute(this);
        }
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    public abstract bool IsPlayerNearby();
    public abstract bool IsTurretNearby();
    public abstract void MoveTowardsGoal();
    public abstract void AttackPlayer();
    public abstract void AttackTurret();
}
