using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeyserTower : TowerBehaviour
{
    RaycastHit2D hit;

    public override void Update()
    {
        if (attackTime >= tower.attackRate)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, 100f, mask);

            if (hit.collider != null)
            {
                Fire();
                attackTime = 0;
            }
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }
    public override void Fire()
    {
        Instantiate(tower.projectile, new Vector3(hit.transform.position.x, transform.position.y + 0.1f, transform.position.z), Quaternion.identity);
    }
}



