using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamTower : MonoBehaviour
{
    [SerializeField]
    private TowerObjects currentTower;
    float attackTime;


    private void Start()
    {
        attackTime = 0;
    }
    void Update()
    {
        if (attackTime >= currentTower.attackRate)
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
