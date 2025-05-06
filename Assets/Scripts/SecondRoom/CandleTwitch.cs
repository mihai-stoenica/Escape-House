using UnityEngine;

public class CandleTwitch : MonoBehaviour
{
    private Light light;
    public float minIntensity = 1.5f;
    public float maxIntensity = 3f;
    public float flickerSpeed = 0.1f;
    private float timer;

    private bool beamIsHitting = false;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        if(light == null)
        {
            Debug.LogError("No candleLight");
        }
        timer = flickerSpeed;
        light.intensity = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (beamIsHitting)
        {
            Flicker();
        }
        else
        {
            light.intensity = 0f;
        }

    }

    void Flicker()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            light.intensity = Random.Range(minIntensity, maxIntensity);
            timer = flickerSpeed;
        }
    }

    public void SetBeamIsHitting(bool isHitting)
    {
        beamIsHitting = isHitting;
        if (!isHitting)
        {
            light.intensity = 0f; 
        }
    }
}
