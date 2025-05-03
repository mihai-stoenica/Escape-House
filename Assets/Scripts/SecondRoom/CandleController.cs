using UnityEngine;
using TMPro;

public class CandleController : MonoBehaviour
{
    private Light candleLight; 
    private TextMeshPro textToReveal; 
    public float lightFadeSpeed = 1.2f;
    public float textFadeSpeed = 1.2f;

    private float targetLightIntensity = 3f;
    private float currentLightIntensity = 0f;

    private float targetTextAlpha = 1f;
    private float currentTextAlpha = 0f;

    private bool beamIsHitting = false;

    private string[] morseCodes = { ".... ___ .._ ... .", ". ... -.-. .- .--. .", ".-. . .- .-.. .. - -.--" };

    void Start()
    {
        candleLight = GetComponentInChildren<Light>();
        textToReveal = GetComponentInChildren<TextMeshPro>();
        if (candleLight != null)
        {
            candleLight.intensity = 0f;
        }
        else {
            Debug.LogError("No candleLight");
        }

        if (textToReveal != null)
        {

            Color c = textToReveal.color;
            c.a = 0f;
            textToReveal.SetText(morseCodes[Random.Range(0, morseCodes.Length)]);
            textToReveal.color = c;
        }
        else
        {
            Debug.LogError("No text");
        }
    }

    void Update()
    {
        if (beamIsHitting)
        {
            currentLightIntensity = Mathf.MoveTowards(currentLightIntensity, targetLightIntensity, Time.deltaTime * lightFadeSpeed);
            candleLight.intensity = currentLightIntensity;

            currentTextAlpha = Mathf.MoveTowards(currentTextAlpha, targetTextAlpha, Time.deltaTime * textFadeSpeed);
            Color c = textToReveal.color;
            c.a = currentTextAlpha;
            textToReveal.color = c;
        }
    }

    public void BeamHit()
    {
        beamIsHitting = true;
    }
}


//HOUSE = .... ___ .._ ... .
//ESCAPE = . ... -.-. .- .--. .
//REALITY = .-. . .- .-.. .. - -.--