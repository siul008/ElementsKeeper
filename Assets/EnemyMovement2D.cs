using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float health = 100f;

    void Update()
    {
        // If the player is not in the way, move forward
        // If no tower is on the way
        // If the enemy is not at the endpoint
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
