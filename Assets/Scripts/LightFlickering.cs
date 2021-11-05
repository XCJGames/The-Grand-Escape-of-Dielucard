using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField]
    private float flickeringInterval = 0.5f;
    private float timer;
    private Light2D lightComponent;
    private float lightIntensity;
    private void Awake()
    {
        timer = flickeringInterval;
    }
    private void Start()
    {
        lightComponent = GetComponent<Light2D>();
        lightIntensity = lightComponent.intensity;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            float min = lightIntensity * 0.8f;
            float max = lightIntensity * 1.2f;
            lightComponent.intensity = Random.Range(min, max);
            timer = flickeringInterval;
        }
    }
}
