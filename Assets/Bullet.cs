using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMovement2D>().TakeDamage(34);
            Destroy(gameObject);
        }
    }
}
