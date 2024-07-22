using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    Vector2 areaOfEffect;
    [SerializeField]
    float damage;
    Vector3 destination = Vector3.zero;
    [SerializeField]
    float speed;

    private void Update()
    {
        if (destination == Vector3.zero)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            OnImpact();
        }
    }

    public void SetDestination(Vector3 _destination)
    {
        destination = _destination;
    }
    public void OnImpact()
    {
       var colliders = Physics2D.OverlapBoxAll(transform.position, areaOfEffect, 0);
       Debug.Log(colliders.Length);
       Enemy[] enemies = colliders
            .Where(collider => collider.CompareTag("Enemy"))
            .Select(collider => collider.GetComponent<Enemy>())
            .ToArray();
        Debug.Log(enemies.Length);
        foreach (Enemy enemy in enemies)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
