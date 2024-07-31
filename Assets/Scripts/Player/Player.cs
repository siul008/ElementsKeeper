using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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
    public float craftingTime;
    public float towerPickupDuration;
    public float initialFragments;
    float towerPickupTime;

    [Header("Editor Assignation")]
    [SerializeField] private Transform cursorHolder;
    [SerializeField] private Color carryingColor;
    [SerializeField] private Color towerColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] TextMeshProUGUI state;
    [SerializeField] GameObject cursorPrefab;
    [SerializeField] GameObject craftTableUI;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private ParticleSystem deathEffect;

    GameObject cursor;
    Rigidbody2D rb;
    [SerializeField] SpriteRenderer sRenderer;
    GameObject enemyTarget;
    Vector2 moveDir;
    PlayerState currentState = null;

    bool isMoving;
    bool isCarrying;
    bool isNearTransmute;
    bool wasCarrying = false;
    bool spaceRelease = true;
    bool paused;

    GameObject lastTower = null;
    GameObject currentTower = null;
    GameObject currentShadow;
    GameObject carriedTower = null;
    
    void Start()
    {
        paused = false;
        rb = GetComponent<Rigidbody2D>();

        spaceRelease = true;
        enemyTarget = null;
        currentHealth = maxHealth;
        cursor = Instantiate(cursorPrefab, transform.position, Quaternion.identity, cursorHolder);
        cursorHolder.localScale = Grid.Instance.GetTileSize();

        ChangeState(new PlayerIdleState());
        UpdateProgBar(0, towerPickupDuration);

        for(int i = 0; i < initialFragments; i++)
        {
            InventoryManager.Instance.AddFragment();
        }
    }

    void Update()
    {
        RegenHealth();
        CheckTowerOnTile();
        ManageShadow();
        if (isMoving && towerPickupTime != 0)
        {
            towerPickupTime = 0;
        }

        currentState.Execute(this);
        if (!spaceRelease)
        {
            TowerInteraction();
        }
        if (spaceRelease)
        {
            towerPickupTime = 0;
        }
        if (!isNearTransmute)
        {
            UpdateProgBar(towerPickupTime, towerPickupDuration);
        }
    }

    void TowerInteraction()
    {
        Tile t = GetTile();

        if (currentTower && carriedTower == null && spaceRelease == false)
        {
            if (towerPickupTime >= towerPickupDuration)
            {
                isCarrying = true;
                carriedTower = currentTower;
                if (currentTower.GetComponent<TowerBehaviour>() != null)
                {
                    currentTower.GetComponent<TowerBehaviour>().ResetAttackTime();
                }
                StartCoroutine(SoundManager.Instance.PlaySfx("TowerCarry", 0f));
                currentTower.SetActive(false);
                t.RemoveTower(false);
                towerPickupTime = 0;
                spaceRelease = true;
            }
            else
            {
                ShowProgBar();
                towerPickupTime += Time.deltaTime;
            }
            UpdateProgBar(towerPickupTime, towerPickupDuration);

        }
        else if (t && !t.GetTower() && spaceRelease == false)
        {
            if (carriedTower != null)
            {
                t.SetTower(carriedTower);
                carriedTower.SetActive(true);
                StartCoroutine(SoundManager.Instance.PlaySfx("TowerDrop", 0f));
                isCarrying = false;
                carriedTower = null;
                spaceRelease = true;
            }
            else if (t.GetAssignable())
            {
                TowerObjects tower = InventoryManager.Instance.GetSelectedTower();
                if (tower)
                {
                    t.SetTower(tower);
                    spaceRelease = true;
                    StartCoroutine(SoundManager.Instance.PlaySfx("TowerSpawn", 0f));
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        }
    }
    public void FaceLeft()
    {
        sRenderer.flipX = false;
    }
    public void FaceRight()
    {
        sRenderer.flipX  = true;
    }

    public bool IsTowerPlacementValid()
    {
        return (true);
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

    public Tile GetTile()
    {
        //return Grid.Instance.GetTileAtPos(transform.position + new Vector3(1.1f * (sRenderer.flipX ? 1 : -1) , 0, 0));
        return (Grid.Instance.GetTileAtPos(transform.position));
    }
    
    void CheckTowerOnTile()
    {
        Tile t = GetTile();
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
        Tile t = GetTile();
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

    public void SetCraftingTableUIVisibility(bool visible)
    {
        craftTableUI.SetActive(visible);
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
    
    public void OnPause(InputValue value)
    {
        if (currentState is PlayerTransmuteState)
        {
            LeaveTransmuteTable();
        }
        else
        {
            if (!paused)
            {
                PauseGame();
                PauseController.Instance.DisplayMenuPause();
            }
            else
            {
                UnpauseGame();
                PauseController.Instance.UnpauseCanvas();
            }
        }
    }


    public void LeaveTransmuteTable()
    {
        Debug.Log("Leave");
        ChangeState(new PlayerIdleState());
        UnpauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        moveDir = Vector2.zero;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        paused = false;
    }

    public void OnMove(InputValue value)
    {
        if (paused)
        {
            return;
        }
        var v = value.Get<Vector2>();
        moveDir.x = v.x;
        moveDir.y = v.y;
        if (v.x < 0)
        {
            FaceLeft();
        }
        else if (v.x > 0)
        {
            FaceRight();
        }
        else if (v.y == 0)
        {
            isMoving = false;
            return ;
        }
        isMoving = true;
    }

    public void OnInteract(InputValue value)
    {
        if (paused)
        {
            return;
        }

        if (isNearTransmute && !paused)
        {
            ChangeState(new PlayerTransmuteState());
            
        }
        else
        {
            spaceRelease = !value.isPressed;
        }
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
        //UpdateHealthBar();
    }
    public void Die()
    {
        GetComponent<Animator>().SetTrigger("Death");
        deathEffect.Play();
        //Destroy(gameObject);
    }
   /* public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }*/

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
       // UpdateHealthBar();
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPoint.position;
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
