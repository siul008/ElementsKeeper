using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private TowerObjects currentTower;
    float attackTime;
    float health;
    Slider energyBar;
    public LayerMask mask;
    

    private void Start()
    {
        energyBar = GetComponentInChildren<Slider>();
        attackTime = 0;
        health = currentTower.maxHealth;
        UpdateEnergyBar();
    }

    bool isEnemyInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 100f, mask);
        if (hit)
        {
            return (true);
        }
        return (false);
    }

    void Update()
    {
        if (attackTime >= currentTower.attackRate && isEnemyInFront())
        {
            Fire();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        UpdateEnergyBar();
    }

    void Fire()
    {
       GameObject bullet = Instantiate(currentTower.projectile, transform.position, transform.rotation);
       bullet.GetComponent<Bullet>().SetDamage(currentTower.damage);
       bullet.GetComponent<SpriteRenderer>().color = currentTower.bulletColor;
    }

    void UpdateEnergyBar()
    {
        energyBar.maxValue = currentTower.maxHealth;
        energyBar.value = health;
    }
}
