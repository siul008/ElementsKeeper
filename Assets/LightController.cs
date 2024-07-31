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
    bool intensitySet = false;
    bool fallOffSet = false;
    bool scaleSet = false;

    private void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            TurnOnLight();
        }
        if (Input.GetKey(KeyCode.F))
        {
            TurnOffLight();
        }
        if (isTurningOff)
        {
            if (lightHolder.intensity + 0.1f * Time.deltaTime < intensityOff)
            {
                lightHolder.intensity += 0.1f * Time.deltaTime;
            }
            else
            {
                lightHolder.intensity = intensityOff;
                intensitySet = true;
            }
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
            if (scaleSet && fallOffSet && intensitySet)
            {
                Debug.Log("Turned off finished");
                isTurningOff = false;
            }
        }

        else if (isTurningOn)
        {
            if (lightHolder.intensity + 0.1f * Time.deltaTime < intensityOn)
            {
                lightHolder.intensity += 0.1f * Time.deltaTime;
            }
            else
            {
                lightHolder.intensity = intensityOn;
                intensitySet = true;
            }
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
            if (scaleSet && fallOffSet && intensitySet)
            {
                Debug.Log("Turning on finished");
                isTurningOn = false;
            }
        }
    }

    public void TurnOffLight()
    {
        isTurningOn = false;
        isTurningOff = true;
        intensitySet = false;
        scaleSet = false;
        fallOffSet = false;
        Debug.Log("Turned off called");
    }

    public void TurnOnLight()
    {
        Debug.Log("Turn on caled");
        isTurningOn = true;
        isTurningOff = false;
        intensitySet = false;
        scaleSet = false;
        fallOffSet = false;
    }

    public float GetTurnOffDuration()
    {
        return 5f;
    }
}
