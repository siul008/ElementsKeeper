using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    [SerializeField]
    private TowerObjects currentTower;
    public LayerMask mask;
    [SerializeField]
    float range;
    [SerializeField]
    GameObject flamethrow;

    bool IsEnemyInFront()
    {
        return (Physics2D.Raycast(transform.position, Vector2.right, range, mask));
    }


    void Update()
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

    void Fire()
    {
        flamethrow.SetActive(true);
    }
}


