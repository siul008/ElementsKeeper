using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudPound : MonoBehaviour
{
    [SerializeField]
    float slowFactor;
    [SerializeField]
    float lifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().SlowEnemy(slowFactor);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().UnslowEnemy();
        }
    }
}
