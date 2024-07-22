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
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Slider progBar;
    bool isMoving;
    bool isCarrying;
    bool isTowerSelected;
    bool isNearTransmute;
    GameObject enemyTarget;
    GameObject movedTower = null;
    public TextMeshProUGUI state;
    public float attackTime;
    public float attackInterval;
    public float craftingTime;

    Vector2 moveDir;

    private PlayerState currentState = null;

    void Start()
    {
        enemyTarget = null;
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        ChangeState(new PlayerIdleState());
        UpdateHealthBar();
        ChangeStateText("None");
        HideProgBar();
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

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
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
        else if (moveDir.y == 0)
        {
            isMoving = false;
            return ;
        }
        isMoving = true;
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

    public void HideProgBar()
    {
        progBar.gameObject.SetActive(false);
    }
    public void ShowProgBar()
    {
        progBar.gameObject.SetActive(true);
    }

    public void UpdateProgBar(float _value, float _maxValue)
    {
        progBar.value = _value;
        progBar.maxValue = _maxValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyTarget == null && collision.gameObject.CompareTag("Enemy"))
        {
            enemyTarget = collision.gameObject;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enemyTarget == null && collision.gameObject.CompareTag("Enemy"))
        {
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
        else if (collision.gameObject.CompareTag("Craft"))
        {
            isNearTransmute = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Craft"))
        {
            isNearTransmute = false;
        }
    }
}
