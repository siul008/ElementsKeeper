using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudProjectile : TowerProjectile
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] GameObject mudPound;

    bool hitEnemy = false;

    void Start()
    {
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!hitEnemy)
            {
                hitEnemy = true;
                other.GetComponent<Enemy>().TakeDamage(damage);
                Instantiate(mudPound, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
