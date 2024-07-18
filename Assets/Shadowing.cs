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
        bool playerInShadow = false;
        foreach (Light light in lights)
        {
            Vector3 directionToPlayer = player.position - light.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (Physics.Raycast(light.transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer))
            {
                if (!hit.transform.CompareTag("Player"))
                {
                    playerInShadow = true;
                    break;
                }
            }
            else
            {
                playerInShadow = true;
                break;
            }
        }
        if (playerInShadow)
        {
            Debug.Log("Player in shadow");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        foreach (Light light in lights)
        {
            Vector3 directionToPlayer = player.position - light.transform.position;

            Gizmos.DrawLine(light.transform.position, player.position);
        }
    }
}


