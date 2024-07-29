using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderProj : TowerProjectile
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float knockbackForce;
    List<GameObject> enemiesHit = new List<GameObject>();
    [SerializeField] int stateBeforeDestroy = 3;
    [SerializeField] float damageReductionPerState = 0.8f;
    [SerializeField] int enemiesToDecreaseState;
     int enemyHitCount;


    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    void Start()
    {
        enemyHitCount = 0;
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
            enemyHitCount++;
            if (enemyHitCount >= enemiesToDecreaseState)
            {
                damage *= damageReductionPerState;
                enemyHitCount = 0;
                stateBeforeDestroy--;
                if (stateBeforeDestroy <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}


