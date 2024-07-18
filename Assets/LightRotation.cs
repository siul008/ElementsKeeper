using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    float lightRotation = 90;
    float lightSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightRotation += Time.deltaTime * lightSpeed;
        transform.rotation = Quaternion.Euler(42, lightRotation, 0);
        if(lightRotation > 270)
        Debug.Log("loser");
    }
}
