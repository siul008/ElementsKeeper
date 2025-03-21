using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private TowerObjects currentTower;
    float health;
    [SerializeField]
    SpriteRenderer sRenderer;

    private void Start()
    {
        health = currentTower.maxHealth;
    }

    public TowerObjects GetScriptable()
    {
        return currentTower;
    }

    public void Die()
    {
        SoundManager.Instance.InstantPlaySfx("TowerDestroy", false);
        transform.parent.GetComponent<Tile>().RemoveTower(true);
    }
    
    public void TakeDamage(float damage)
    {
        StartCoroutine(SoundManager.Instance.PlaySfx("EnemyAttack", 0));
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }


    public void SetTransparentTower(Color color)
    {
        if (sRenderer)
        {
            sRenderer.color = color;
            sRenderer.sortingOrder = 6;
        }
    }

    public void SetOpaqueTower()
    {
        Color color = Color.white;
        color.a = 1;
        sRenderer.color = color;
        sRenderer.sortingOrder = 0;
    }

    public void SetGhostTower(Color color)
    {
        GetComponent<Animator>().enabled = false;
        SetTransparentTower(color);
        TowerBehaviour behaviour = GetComponent<TowerBehaviour>();
        if (behaviour != null)
        {
            behaviour.enabled = false;
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}