using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : TowerProjectile
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float knockbackForce;
    List<GameObject> enemiesHit = new List<GameObject>();

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
        if (other.tag == "Enemy" && !enemiesHit.Contains(other.gameObject))
        {
            enemiesHit.Add(other.gameObject);
            other.GetComponent<Enemy>().TakeDamage(damage);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
            if (enemiesHit.Count > 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
