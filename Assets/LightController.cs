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

    public float turnOffDuration = 100f;
    public float turnOnDuration = 100f;

    public IEnumerator TurnOffLight()
    {
        yield return new WaitForSeconds(turnOffDuration);
        transform.localScale = scaleOff;
        lightHolder.intensity = intensityOff;
        lightHolder.falloffIntensity = fallOffOff;
    }

    public IEnumerator TurnOnLight()
    {
        yield return new WaitForSeconds(turnOnDuration);
        transform.localScale = scaleOn;
        lightHolder.intensity = intensityOn;
        lightHolder.falloffIntensity = fallOffOn;
    }

    public float GetTurnOffDuration()
    {
        return (turnOffDuration);
    }
}
