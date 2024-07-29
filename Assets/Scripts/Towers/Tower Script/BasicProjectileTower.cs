using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicProjectileTower : TowerBehaviour
{
    
    public override void Fire()
    {
        if (tower.type == TowerObjects.Types.Fire)
        {
            SoundManager.Instance.InstantPlaySfx("Fireball1", true);
        }
        Instantiate(tower.projectile, transform.position, transform.rotation).GetComponent<TowerProjectile>().SetupProjectile(tower.damage, tower.attackRate);
    }
}
