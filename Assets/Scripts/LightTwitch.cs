using UnityEngine;
using System.Collections;

public class LightTwitch : MonoBehaviour
{
    private Light lightComponent;
    public float twitchMax = 5.0f;
    public float twitchMin = 2.0f;
    private float baseIntensity;
    public int twitchCount = 4;

    public float twitchDelay = 0.1f; 
    public float cycleDelay = 3.0f; 

    void Start()
    {
        lightComponent = GetComponent<Light>();
        
        if (lightComponent == null)
        {
            Debug.LogError("No Light component");
            enabled = false; 
            return;
        }
        baseIntensity = lightComponent.intensity;   
        
        StartCoroutine(TwitchCycle());
    }

    private IEnumerator TwitchCycle()
    {
        while (true)
        {
            lightComponent.intensity = twitchMax;
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < twitchCount; i++)
            {
                lightComponent.intensity = twitchMax;
                yield return new WaitForSeconds(twitchDelay); 
                lightComponent.intensity = twitchMin;
                yield return new WaitForSeconds(twitchDelay); 
            }

            lightComponent.intensity = baseIntensity;
            yield return new WaitForSeconds(cycleDelay); 
        }
    }
}