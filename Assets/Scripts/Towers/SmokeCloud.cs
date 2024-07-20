using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCloud : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float damageRate;
    [SerializeField]
    float decayTime;
    float currentTime;
    List<GameObject> enemiesAffected = new List<GameObject>();
    List<GameObject> enemiesToRemove = new List<GameObject>();


    private void Start()
    {
        Destroy(gameObject, decayTime);
        currentTime = 0;
    }
    private void Update()
    {
        if (currentTime > damageRate)
        {
            currentTime = 0;
            foreach (GameObject enemy in enemiesAffected)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            foreach(GameObject enemy in enemiesToRemove)
            {
                enemiesAffected.Remove(enemy);
            }
            enemiesToRemove.Clear();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesAffected.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") 
            && enemiesAffected.Contains(collision.gameObject) 
                && !enemiesToRemove.Contains(collision.gameObject))
        {
            enemiesToRemove.Add(collision.gameObject);
        }
    }
}
