using UnityEngine;
using TMPro;
//using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine.UI;



public class WeightScale : MonoBehaviour
{
    public TextMeshPro hintTargetWeight;
    public TextMeshPro weightText;
    private bool isWeightObjectOnScale = false;
    private List<Rigidbody> objectsOnScale = new List<Rigidbody>();
    public GravityManager GravityManager;

    private List<float> availableWeights = new List<float> { 0.5f, 1f, 7f, 10f, 11.5f, 12f, 15f, 22.5f };
    private float randomGravityScale;
    private float targetWeight;

    private float gravityScale;

    private float totalWeight = 0f;

    public ChestController chestController;
    private bool isChestOpen = false;

    void Start()
    {
        gravityScale = GravityManager.gravityScale;
        randomGravityScale = Random.Range(1, 101) * 0.1f;
        List<float> selectedWeights = GetRandomSubset();
        float sum = 0;
        foreach (float w in selectedWeights)
        {
            sum += w;
        }

        targetWeight = sum * randomGravityScale;
        hintTargetWeight.text = "Target Weight: " + targetWeight.ToString("F2") + " N";
        Debug.Log("Selected weights: " + string.Join(", ", selectedWeights));
        Debug.Log("Target weight (gravity adjusted): " + targetWeight.ToString("F2"));
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeightObject"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null && !objectsOnScale.Contains(rb))
                objectsOnScale.Add(rb);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WeightObject"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
                objectsOnScale.Remove(rb);
        }
    }

    void Update()
    {
        gravityScale = GravityManager.gravityScale;
        totalWeight = 0f;
        foreach (Rigidbody rb in objectsOnScale)
        {
            if (rb != null)
                totalWeight += rb.mass * gravityScale;
        }
        weightText.text = "Weight: " + totalWeight.ToString("F2") + " N";
        if (Mathf.Abs(totalWeight - targetWeight) < 0.05f && !isChestOpen)
        {
            Debug.Log("Correct weight achieved!");
            isChestOpen = true;
            chestController.ToggleChest();
            return;
        }



        
    }

    List<float> GetRandomSubset()
    {
        List<float> result = new List<float>();
        foreach (float weight in availableWeights)
        {
            if (Random.Range(0,2) == 1)  
            {
                result.Add(weight);
            }
        }

        if (result.Count == 0)
        {
            result.Add(availableWeights[Random.Range(0, availableWeights.Count)]);
        }

        return result;
    }
}
//