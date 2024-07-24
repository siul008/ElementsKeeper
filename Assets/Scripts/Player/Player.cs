using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Slider Bars")]
    [SerializeField] Slider healthBar;
    [SerializeField] Slider progBar;

    [Header("Attributes")]
    public float moveSpeed = 5f;
    public float maxHealth;
    public float currentHealth;
    public float healthRegen;
    public float attackTime;
    public float attackInterval;
    public float craftingTime;
    public float damage;

    [Header("Editor Assignation")]
    [SerializeField] private Transform cursorHolder;
    [SerializeField] private Color carryingColor;
    [SerializeField] private Color towerColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] TextMeshProUGUI state;
    [SerializeField] GameObject cursorPrefab;

    GameObject cursor;
    Rigidbody2D rb;
    SpriteRenderer sRenderer;
    GameObject enemyTarget;
    Vector2 moveDir;
    PlayerState currentState = null;

    bool isMoving;
    bool isCarrying;
    bool isNearTransmute;
    bool wasCarrying = false;

    GameObject lastTower = null;
    GameObject currentTower = null;
    GameObject currentShadow;
    GameObject carriedTower = null;

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
        cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity, cursorHolder);
        cursorHolder.localScale = Grid.Instance.GetTileSize();
    }

    void Update()
    {
        HandleMovement();
        RegenHealth();
        CheckTowerOnTile();
        ManageShadow();
        currentState.Execute(this);
        if (attackTime < attackInterval)
        {
            attackTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryPlaceTower();
        }
    }

    void TryPlaceTower()
    {
        Tile t = Grid.Instance.GetTileAtPos(transform.position);

        if (currentTower && carriedTower == null)
        {
            isCarrying = true;
            carriedTower = currentTower;
            currentTower.SetActive(false);
            t.RemoveTower(false);
        }
        else if (t && !t.GetTower())
        {
            if (carriedTower != null)
            {
                t.SetTower(carriedTower);
                carriedTower.SetActive(true);
                isCarrying = false;
                carriedTower = null;
            }
            else
            {
                TowerObjects tower = InventoryManager.Instance.GetSelectedTower();
                if (tower)
                {
                    t.SetTower(tower);
                }
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
        return (true);
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

    void CheckTowerOnTile()
    {
        Tile t = Grid.Instance.GetTileAtPos(transform.position);
        if (!t)
        {
            cursor.SetActive(false);
            if (lastTower)
            {
                lastTower.GetComponent<TowerScript>().SetOpaqueTower();
                currentTower = null;
                lastTower = null;
            }  
            return;
        }
        cursor.SetActive(true);
        cursor.transform.position = t.transform.position;

        lastTower = currentTower;
        currentTower = t.GetTower();
        if (currentTower)
        {
            currentTower.GetComponent<TowerScript>().SetTransparentTower(hoverColor);
        }
        if (lastTower && lastTower != currentTower)
            lastTower.GetComponent<TowerScript>().SetOpaqueTower();
    }

    void ManageShadow()
    {
        GameObject selected = InventoryManager.Instance.GetSelectedTowerGO();
        Tile t = Grid.Instance.GetTileAtPos(transform.position);
        if (t && !t.GetTower() && (selected || isCarrying))
        {
            if (currentShadow)
                currentShadow.SetActive(true);
            if (isCarrying)
            {
                if ((carriedTower && !wasCarrying) || !currentShadow)
                {
                    Destroy(currentShadow);
                    SpawnShadow(carriedTower, t.transform.position, carryingColor);
                }
                else if (currentShadow && carriedTower && wasCarrying && currentShadow.transform.position != t.transform.position)
                {
                    currentShadow.transform.position = t.transform.position;
                }
                wasCarrying = true;
            }
            else
            {
                if (wasCarrying)
                {
                    Destroy(currentShadow);
                    SpawnShadow(selected, t.transform.position, towerColor);
                }
                else
                {
                    if (!currentShadow)
                    {
                        SpawnShadow(selected, t.transform.position, towerColor);
                    }
                    else
                    {
                        if (currentShadow.GetComponent<TowerScript>().GetScriptable() != selected.GetComponent<TowerScript>().GetScriptable())
                        {
                            Destroy(currentShadow);
                            SpawnShadow(selected, t.transform.position, towerColor);
                        }
                        else if (currentShadow.transform.position != t.transform.position)
                        {
                            currentShadow.transform.position = t.transform.position;
                        }
                    }
                }
                wasCarrying = false;
            }
            
        }
        else if (currentShadow)
        {
            if (isCarrying)
                currentShadow.SetActive(false);
            else
                Destroy(currentShadow);
        }
    }

    void SpawnShadow(GameObject g, Vector3 pos, Color color)
    {
        if (g)
        {
            currentShadow = Instantiate(g, pos, Quaternion.identity, cursorHolder);
            currentShadow.GetComponent<TowerScript>().SetGhostTower(color);
            currentShadow.SetActive(true);
        }
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
