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
    [SerializeField]
    protected float baseMoveSpeed;
    protected float moveSpeed;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float dropChance;
    [SerializeField]
    public float attackInterval;
    [SerializeField] protected GameObject particles;
    [SerializeField]
    protected GameObject voidFragment;
    public GameObject turret;
    public float attackTime;
    Slider healthBar;
    public GameObject player;

    protected EnemyState currentState;

    private void Awake()
    {
        turret = null;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        player = GameObject.Find("Player");
        attackTime = attackInterval;
        moveSpeed = baseMoveSpeed;
    }

    private void Start()
    {
        ChangeState(new WalkingState());
    }
    private void Update()
    {
        if (attackTime < attackInterval)
        {
            attackTime += Time.deltaTime;
        }
        if (currentState != null)
        {
            currentState.Execute(this);
        }
    }

    public void SetStunState(float dur, float dist, float speed)
    {
        ChangeState(new StunnedState(dur, dist, speed));
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void SlowEnemy(float slowFactor)
    {
        if (moveSpeed < baseMoveSpeed)
        {
            return;
        }
        moveSpeed *= slowFactor;
    }

    public void UnslowEnemy()
    {
        moveSpeed = baseMoveSpeed;
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
    public abstract bool IsFacingPlayer();
    public abstract bool IsTurretNearby();
    public abstract void MoveTowardsGoal();
    public abstract void AttackTurret();
}
