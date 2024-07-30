using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour
{
    [SerializeField]
    protected TowerObjects tower;
    protected float attackTime;
    [SerializeField]
    protected LayerMask mask;

    public virtual bool IsEnemyInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 100f, mask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("MaxRange"))
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public virtual void Update()
    {
        if (attackTime >= tower.attackRate && IsEnemyInFront())
        {
            Fire();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    public void ResetAttackTime()
    {
        attackTime = 0;
    }
    public abstract void Fire();
}
