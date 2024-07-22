using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTower : TowerBehaviour
{
    [SerializeField]
    Vector3 offset;
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
        Meteor meteor = Instantiate(tower.projectile, hit.transform.position + offset, transform.rotation).GetComponent<Meteor>();
        meteor.SetDestination(hit.transform.position);
    }
}


