using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicProjectileTower : MonoBehaviour
{
    [SerializeField] 
    private TowerObjects currentTower;
    public LayerMask mask;
    float attackTime;

    bool IsEnemyInFront()
    {
        return (Physics2D.Raycast(transform.position, Vector2.right, 100f, mask));
    }

    private void Start()
    {
        attackTime = 0;
    }
    void Update()
    {
        if (attackTime >= currentTower.attackRate && IsEnemyInFront())
        {
            Fire();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    void Fire()
    {
       Instantiate(currentTower.projectile, transform.position, transform.rotation);
    }
}
