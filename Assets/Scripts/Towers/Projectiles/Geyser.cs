using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : TowerProjectile
{
    BoxCollider2D boxCollider;
    [SerializeField] float stunDuration;
    [SerializeField] float bumpDistance;
    [SerializeField] float bumpSpeed;

    List<GameObject> enemiesHit = new List<GameObject>();
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateHitbox()
    {
        boxCollider.enabled = true;
    }

    public void DesactivateHitbox()
    {
        boxCollider.enabled = false;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !enemiesHit.Contains(collision.gameObject))
        {
            enemiesHit.Add(collision.gameObject);
            collision.GetComponent<Enemy>().TakeDamage(damage);
            collision.GetComponent <Enemy>().SetStunState(stunDuration, bumpDistance, bumpSpeed);
        }
    }
}
