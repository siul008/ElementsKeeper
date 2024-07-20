using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float healthRegen;
    SpriteRenderer sRenderer;
    public float maxHealth;
    public float currentHealth;
    public float damage;
    Slider healthBar;
    bool isMoving;
    bool isCarrying;
    bool isTowerSelected;
    bool isNearTransmute;
    GameObject enemyTarget;
    GameObject movedTower = null;
    TextMeshProUGUI state;
    public float attackTime;
    public float attackInterval;

    private PlayerState currentState = null;

    void Start()
    {
        enemyTarget = null;
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        UpdateHealthBar();
        ChangeStateText("None");
    }

    void Update()
    {
        HandleMovement();
        RegenHealth();
        currentState.Execute(this);
        if (attackTime < attackInterval)
        {
            attackTime += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && IsTowerPlacementValid())
        {
            if (movedTower)
            {
                //place moved turret
            }
            else //if a tower is selected
            {
                //place selected turret
            }
        }
    }
    public void FaceLeft()
    {
        sRenderer.flipY = true;
    }
    public void FaceRight()
    {
        sRenderer.flipY = false;
    }

    public bool IsTowerPlacementValid()
    {
        return (false);
    }
    public void ChangeStateText(string text)
    {
        state.text = text;
    }
    public void ChangeState(PlayerState newState)
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

    public bool PlayerCarryTower()
    {
        return (isCarrying);
    }
    public bool PlayerInMovement()
    {
        return (isMoving);
    }

    public bool PlayerNearTransmute()
    {
        return (isNearTransmute);
    }

    public GameObject GetEnemy()
    {
        return (enemyTarget);
    }


    void HandleMovement()
    {
        Vector2 moveDir;

        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if (moveDir.x < 0)
        {
            FaceLeft();
        }
        else if (moveDir.x > 0)
        {
            FaceRight();
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
