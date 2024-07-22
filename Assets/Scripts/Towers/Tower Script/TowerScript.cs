using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private TowerObjects currentTower;
    float health;
    Slider energyBar;
    [SerializeField]
    SpriteRenderer sRenderer;


    private void Start()
    {
        energyBar = GetComponentInChildren<Slider>();
        health = currentTower.maxHealth;
        UpdateEnergyBar();
    }

    public TowerObjects GetScriptable()
    {
        return currentTower;
    }

    public void Die()
    {
        transform.parent.GetComponent<Tile>().RemoveTower(true);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        energyBar.maxValue = currentTower.maxHealth;
        energyBar.value = health;
    }

    public void SetTransparentTower(Color color)
    { 
        color.a = 0.4f;
        sRenderer.color = color;
        sRenderer.sortingOrder = 6;
    }

    public void SetOpaqueTower()
    {
        Color color = sRenderer.color;
        color.a = 1;
        sRenderer.color = color;
        sRenderer.sortingOrder = 0;
    }

    public void SetGhostTower()
    {
        SetTransparentTower(Color.white);
        TowerBehaviour behaviour = GetComponent<TowerBehaviour>();
        if (behaviour != null)
        {
            behaviour.enabled = false;
        }
        this.enabled = false;
    }
}