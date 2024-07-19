using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    bool isInContact = false;
    Slider healthBar;
    PlayerController movement;


    void Start()
    {
        movement = GetComponent<PlayerController>();
        healthBar = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (damage < 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isInContact = true;
            Debug.Log("In contact");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isInContact = false;
            Debug.Log("Not in contact");
        }
    }
}
