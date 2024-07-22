using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    public float xThreshold = 2.0f;
    public float cameraSpeed = 2.0f;
    public float maxX;
    public float minX;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;

        float xDist = player.position.x - transform.position.x;

        if (Mathf.Abs(xDist) > xThreshold)
        {
            newPos.x = Mathf.Lerp(transform.position.x, player.position.x, cameraSpeed * Time.deltaTime);
        }
        
        if (newPos.x > maxX)
        {
            newPos.x = maxX;
        }
        if (newPos.x < minX)
        {
            newPos.x = minX;
        }
        transform.position = newPos;
    }
}
