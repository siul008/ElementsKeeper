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

    public float turnOffDuration = 1f; // Duration in seconds
    public float turnOnDuration = 1f;

    private bool isTurningOff = false;
    private bool isTurningOn = false;
    private float transitionProgress = 0f;

    private void Start()
    {
        // Optional initialization
    }

    void Update()
    {
        if (isTurningOff)
        {
            transitionProgress += Time.deltaTime / turnOffDuration;
            transitionProgress = Mathf.Clamp01(transitionProgress); // Ensure progress stays within 0 and 1
            UpdateLightProperties(transitionProgress);

            if (transitionProgress >= 1f)
            {
                isTurningOff = false; // Stop turning off
            }
        }
        else if (isTurningOn)
        {
            transitionProgress += Time.deltaTime / turnOnDuration;
            transitionProgress = Mathf.Clamp01(transitionProgress); // Ensure progress stays within 0 and 1
            UpdateLightProperties(transitionProgress);

            if (transitionProgress >= 1f)
            {
                isTurningOn = false; // Stop turning on
            }
        }
    }

    public void TurnOffLight()
    {
        Debug.Log("Turn off light");
        if (!isTurningOff && !isTurningOn) // Prevent starting if already transitioning
        {
            isTurningOff = true;
            transitionProgress = 0f;
        }
    }

    public void TurnOnLight()
    {
        Debug.Log("Turn on light");
        if (!isTurningOn && !isTurningOff) // Prevent starting if already transitioning
        {
            isTurningOn = true;
            transitionProgress = 0f;
        }
    }

    private void UpdateLightProperties(float progress)
    {
        lightHolder.intensity = Mathf.Lerp(intensityOn, intensityOff, progress);
        lightHolder.pointLightInnerRadius = Mathf.Lerp(fallOffOn, fallOffOff, progress); // Assuming this corresponds to falloff size
        lightGO.transform.localScale = Vector3.Lerp(scaleOn, scaleOff, progress);
    }

    public float GetTurnOffDuration()
    {
        return turnOffDuration;
    }
}
