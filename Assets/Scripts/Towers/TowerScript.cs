using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private TowerObjects currentTower;
    float health;
    Slider energyBar;


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

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        UpdateEnergyBar();
    }

    void UpdateEnergyBar()
    {
        energyBar.maxValue = currentTower.maxHealth;
        energyBar.value = health;
    }
}