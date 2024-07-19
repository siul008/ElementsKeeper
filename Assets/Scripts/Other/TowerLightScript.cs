using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLightScript : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate = 1f;
    float cooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            cooldown = fireRate;
            Fire();
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
