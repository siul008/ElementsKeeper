using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadowing : MonoBehaviour
{
    public Light[] lights;
    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerInShadow = true;
        foreach (Light light in lights)
        {
            Vector3 directionToPlayer = player.position - light.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (!Physics.Raycast(light.transform.position, directionToPlayer, distanceToPlayer))
            {
                playerInShadow = false;
                break;
            }
        }
        if (playerInShadow)
        {
            Debug.Log("player in shadow");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Light light in lights)
        {
            Vector3 directionToPlayer = player.position - light.transform.position;

            Gizmos.DrawLine(light.transform.position, directionToPlayer);
        }
    }
}


