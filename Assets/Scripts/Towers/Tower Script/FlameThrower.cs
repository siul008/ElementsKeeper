using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : TowerBehaviour
{
    
    [SerializeField]
    float range;
    [SerializeField]
    GameObject flamethrow;

    private void Start()
    {
        flamethrow.GetComponent<TowerProjectile>().SetupProjectile(tower.damage, tower.attackRate);
    }
    public override bool IsEnemyInFront()
    {
        return (Physics2D.Raycast(transform.position, Vector2.right, range, mask));
    }

    public override void Update()
    {
        if (IsEnemyInFront())
        {
            Fire();
        }
        else
        {
            StopFire();
        }
    }

    void StopFire()
    {
        flamethrow.SetActive(false);
    }

    public override void Fire()
    {
        flamethrow.SetActive(true);
    }
}


