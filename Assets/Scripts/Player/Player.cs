using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float healthRegen;
    SpriteRenderer sRenderer;
    public float maxHealth;
    public float currentHealth;
    Slider healthBar;
    bool isMoving;
    bool isCarrying;
    bool isTowerSelected;
    bool isEnemyInContact;
    GameObject enemyTarget;

    private PlayerState currentState;

    void Start()
    {
        enemyTarget = null;
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        HandleMovement();
        RegenHealth();

        if (enemyTarget && !isMoving)
        {
            enemyTarget.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    void HandleMovement()
    {
        Vector2 moveDir;

        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if (moveDir.x < 0)
        {
            sRenderer.flipY = true;
        }
        else if (moveDir.x > 0)
        {
            sRenderer.flipY = false;
        }
        else
        {
            isMoving = false;
            return ;
        }
        isMoving = true;
        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    }
    void RegenHealth()
    {
        if (currentHealth + healthRegen > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthRegen;
        }
        UpdateHealthBar();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
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
        Debug.Log("Collided");
        if (enemyTarget == null && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collided");
            enemyTarget = collision.gameObject;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collided");
        if (enemyTarget == null && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collided");
            enemyTarget = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (enemyTarget != null && collision.gameObject == enemyTarget)
        {
            enemyTarget = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            InventoryManager.Instance.AddFragment();
            Destroy(collision.gameObject);
        }
    }
}
