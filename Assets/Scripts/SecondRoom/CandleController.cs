using UnityEngine;
using TMPro;
using Unity.Collections;
using System.Collections.Generic;

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

    private bool beamIsHitting = false, isDoneFading = false;

    //private string[] morseCodes = { ".... ___ .._ ... .", ". ... -.-. .- .--. .", ".-. . .- .-.. .. - -.--" };

    //private int chestCode;
    public ChestController chestController;

    private Dictionary<string, (string morse, int sum)> codeMap = new Dictionary<string, (string, int)>()
    {
        { "HOUSE", (".... ___ .._ ... .", 68) },
        { "ESCAPE", (". ... -.-. .- .--. .", 49) },
        { "REALITY", (".-. . .- .-.. .. - -.--", 90) }
    };

    private static string morseCode = null;

    private CandleTwitch candleTwitch;

    void Start()
    {

        candleTwitch = GetComponent<CandleTwitch>();
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
            if(morseCode == null)
            {
                //morseCode = morseCodes[Random.Range(0, morseCodes.Length)];
                List<string> keys = new List<string>(codeMap.Keys);
                string randomKey = keys[Random.Range(0, keys.Count)];
                morseCode = codeMap[randomKey].morse;
                chestController.unlockCode = codeMap[randomKey].sum;
                Debug.Log($"Selected word: {randomKey}, Sum: {codeMap[randomKey].sum}");
            }

            textToReveal.SetText(morseCode);
            textToReveal.color = c;
        }
        else
        {
            Debug.LogError("No text");
        }
    }

    void Update()
    {
        if (beamIsHitting && !isDoneFading)
        {
            
            currentLightIntensity = Mathf.MoveTowards(currentLightIntensity, targetLightIntensity, Time.deltaTime * lightFadeSpeed);
            candleLight.intensity = currentLightIntensity;
            
            
            currentTextAlpha = Mathf.MoveTowards(currentTextAlpha, targetTextAlpha, Time.deltaTime * textFadeSpeed);
            Color c = textToReveal.color;
            c.a = currentTextAlpha;
            textToReveal.color = c;
            if(currentLightIntensity >= targetLightIntensity && currentTextAlpha >= targetTextAlpha)
            {
                isDoneFading = true;
            }
            //isDoneFading = true;
            if (isDoneFading)
            {
                candleTwitch.SetBeamIsHitting(true);
            }
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