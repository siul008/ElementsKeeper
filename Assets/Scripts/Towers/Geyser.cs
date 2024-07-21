using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] float stunDuration;
    [SerializeField] float bumpDistance;
    [SerializeField] float bumpSpeed;
    [SerializeField] float activatedTime;
    [SerializeField] float lifetime;
    [SerializeField] float damage;

    List<GameObject> enemiesHit = new List<GameObject>();
    void Start()
    {
        Destroy(gameObject, lifetime);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateHitbox()
    {
        boxCollider.enabled = true;
    }

    public IEnumerator DesactivateHitbox()
    {
        yield return new WaitForSeconds(activatedTime);
        boxCollider.enabled = false;
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
