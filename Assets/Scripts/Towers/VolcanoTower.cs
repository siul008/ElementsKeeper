using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoTower : MonoBehaviour
{

    [SerializeField]
    private TowerObjects currentTower;
    public LayerMask mask;
    float attackTime;
    [SerializeField]
    Vector3 offset;

    private void Start()
    {
        attackTime = 0;
    }
    void Update()
    {
        if (attackTime >= currentTower.attackRate)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 100f, mask);

            if (hit.collider != null)
            {
                Fire(hit.transform.position);
                attackTime = 0;
            }
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    void Fire(Vector3 position)
    {
       Meteor meteor = Instantiate(currentTower.projectile, position + offset, transform.rotation).GetComponent<Meteor>();
       meteor.SetDestination(position);
    }
}


