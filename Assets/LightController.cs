using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    bool decreasing;
    bool increasing;
    public Vector3 lightDown = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 lightUp = new Vector3(0.3f, 0.3f, 0.3f);

    private void Start()
    {
        increasing = false;
        decreasing = true;
    }
    void Update()
    {
        if (increasing)
        {
            transform.localScale += lightUp * Time.deltaTime;
            {
                if (transform.localScale.x > 1)
                {
                    transform.localScale = Vector3.one;
                    increasing = false;
                    decreasing = true;
                }
            }
        }
        else if (decreasing)
        {
            transform.localScale -= lightDown * Time.deltaTime;
            if (transform.localScale.x < 0)
            {
                transform.localScale = Vector3.zero;
                decreasing = false;
                increasing = false;
            }
        }
    }

    public void ResetLight()
    {
        increasing = true;
        decreasing = false;
    }
}
