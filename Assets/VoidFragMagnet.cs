using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidFragMagnet : MonoBehaviour
{
    [SerializeField] Transform frag;
    bool magnetized = false;
    Transform player;
    float magnetSpeed;
    float speedFactor;

    void Update()
    {
        if (magnetized)
        {
            magnetSpeed = 4 / Vector3.Distance(transform.position, player.position);
            frag.position = Vector3.Lerp(frag.position, player.position, magnetSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            magnetized = true;
            player = collision.transform;
        }
    }
}
