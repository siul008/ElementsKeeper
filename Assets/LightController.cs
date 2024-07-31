using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] Light2D lightHolder;
    [SerializeField] float intensityOn;
    [SerializeField] float fallOffOn;
    [SerializeField] Vector3 scaleOn = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField] float intensityOff;
    [SerializeField] float fallOffOff;
    [SerializeField] Vector3 scaleOff = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] GameObject lightGO;
    [SerializeField] float fallOfSpeed;
    [SerializeField] float scaleSpeed;


    bool isTurningOff = false;
    bool isTurningOn = false;
    bool fallOffSet = false;
    bool scaleSet = false;

    void Update()
    {
        if (isTurningOff)
        {
            if (lightHolder.shapeLightFalloffSize + fallOfSpeed * Time.deltaTime < fallOffOff)
            {
                lightHolder.shapeLightFalloffSize += fallOfSpeed * Time.deltaTime;
            }
            else
            {
                lightHolder.shapeLightFalloffSize = fallOffOff;
                fallOffSet = true;
            }
            if (lightGO.transform.localScale.x - scaleSpeed * Time.deltaTime > scaleOff.x)
            {
                lightGO.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            }
            else
            {
                lightGO.transform.localScale = scaleOff;
                scaleSet = true;
            }
            if (scaleSet && fallOffSet)
            {
                isTurningOff = false;
            }
        }

        else if (isTurningOn)
        {
            if (lightHolder.shapeLightFalloffSize - fallOfSpeed * Time.deltaTime > fallOffOn)
            {
                lightHolder.shapeLightFalloffSize -= fallOfSpeed * Time.deltaTime;
            }
            else
            {
                lightHolder.shapeLightFalloffSize = fallOffOn;
                fallOffSet = true;
            }
            if (lightGO.transform.localScale.x + scaleSpeed * Time.deltaTime < scaleOn.x)
            {
                lightGO.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            }
            else
            {
                lightGO.transform.localScale = scaleOn;
                scaleSet = true;
            }
            if (scaleSet && fallOffSet)
            {
                isTurningOn = false;
            }
        }
    }

    public void TurnOffLight()
    {
        isTurningOn = false;
        isTurningOff = true;
        scaleSet = false;
        fallOffSet = false;
    }

    public void TurnOnLight()
    {
        isTurningOn = true;
        isTurningOff = false;
        scaleSet = false;
        fallOffSet = false;
    }

    public float GetTurnOffDuration()
    {
        return 10f;
    }
}
