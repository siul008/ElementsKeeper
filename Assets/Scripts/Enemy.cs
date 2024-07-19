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
    [SerializeField]
    protected float damage;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public abstract void TakeDamage(float damage);
    public abstract void Die();

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
}
