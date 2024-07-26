using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int maxHealth = 0;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentHealth--;
            Destroy(other.gameObject);
            SpawnerScript.Instance.EnemyDied();
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player base has been destroyed");
        }
    }
}
