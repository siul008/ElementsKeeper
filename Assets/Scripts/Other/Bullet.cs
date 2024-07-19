using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().TakeDamage(34);
            Destroy(gameObject);
        }
    }
}
