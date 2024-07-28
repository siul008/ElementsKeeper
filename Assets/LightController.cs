using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    bool decreasing;
    bool increasing;
    public Vector3 lightDown = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 lightUp = new Vector3(0.3f, 0.3f, 0.3f);
    public float duration = 100f; 

    private Vector3 initialScale;
    private Vector3 targetScale;
    private float elapsedTime;

    private void Start()
    {
        increasing = false;
        decreasing = true;
        initialScale = transform.localScale;
        targetScale = Vector3.zero;
        elapsedTime = 0f;
    }

    void Update()
    {
        if (increasing)
        {
            transform.localScale += lightUp * Time.deltaTime;
            if (transform.localScale.x > 1)
            {
                transform.localScale = Vector3.one;
                increasing = false;
                decreasing = true;
                elapsedTime = 0f; 
            }
        }
        else if (decreasing)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            if (transform.localScale.x <= 0)
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
        transform.localScale = initialScale; 
        elapsedTime = 0f;
    }
}
