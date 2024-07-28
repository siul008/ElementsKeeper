using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTower : TowerBehaviour
{
    
    public override void Update()
    {
        if (attackTime >= tower.attackRate)
        {
            Fire();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    public override void Fire()
    {
        Instantiate(tower.projectile, transform.position, transform.rotation).GetComponent<TowerProjectile>().SetupProjectile(tower.damage, tower.attackRate); ;
    }
}
