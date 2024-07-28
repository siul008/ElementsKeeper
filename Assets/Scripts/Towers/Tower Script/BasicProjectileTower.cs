using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicProjectileTower : TowerBehaviour
{
    
    public override void Fire()
    {
        Instantiate(tower.projectile, transform.position, transform.rotation).GetComponent<TowerProjectile>().SetupProjectile(tower.damage, tower.attackRate);
    }
}
