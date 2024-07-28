using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    protected float damage;
    protected float attackRate;

    public void SetupProjectile(float dmg, float rate)
    {
        damage = dmg;
        attackRate = rate;
    }
}
