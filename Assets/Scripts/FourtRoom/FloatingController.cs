using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class FloatingController : MonoBehaviour
{
    public TextMeshPro serialsTxt;

    public float floatStrength = 10f;
    public float objectDensity = 1f;

    private Rigidbody rb;
    private float waterTop;
    private float waterBottom;
    private bool inWater = false;
    private float waterDrag = 1f;

    public LightEmmiter[] emmiters;
    public int cutEmmiter = -1;
    private bool hasCut = false;


    public LineRenderer[] wires; 
    public string serial; 
    private Color[] colors = { Color.red, Color.green, Color.blue };
    public Color[] orderedColors;

    void AssignRandomColors()
    {
        List<Color> wireColors = new List<Color>(colors);
        
        wireColors.Add(colors[Random.Range(0, colors.Length)]);
     
        wireColors = wireColors.OrderBy(x => Random.value).ToList();

   
        for (int i = 0; i < wires.Length; i++)
        {
            wires[i].material.color = wireColors[i];

            orderedColors[i] = wireColors[i];
        }

    }

    void GenerateSerial()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        serial = new string(Enumerable.Repeat(chars, 6).Select(s => s[Random.Range(0, s.Length)]).ToArray());
        serialsTxt.text = serial;
        Debug.Log("Aquarium serial: " + serial);
    }

    private void Awake()
    {
        orderedColors = new Color[wires.Length];
        GenerateSerial();
        AssignRandomColors();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterTop = other.bounds.max.y;
            waterBottom = other.bounds.min.y;
            inWater = true;
            rb.linearDamping = waterDrag;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }

    private void Update()
    {
        hasCut = false;
        foreach (LightEmmiter emitter in emmiters)
        {
            if(emitter.isCut)
            {
                hasCut = true;
                cutEmmiter = System.Array.IndexOf(emmiters, emitter);
                break;
            }
        }
        if(!hasCut)
        {
            cutEmmiter = -1;
        }
    }

    void FixedUpdate()
    {
        if (inWater)
        {
            float submergedY = Mathf.Clamp(transform.position.y, waterBottom, waterTop);
            float depth = waterTop - submergedY;

            Vector3 upwardForce = Vector3.up * depth * floatStrength / objectDensity;
            rb.AddForce(upwardForce, ForceMode.Acceleration);
        }
    }

    public void IncreaseDensity()
    {
        objectDensity = Mathf.Clamp(objectDensity + 0.05f, 0.1f, 1f);
    }

    public void DecreaseDensity()
    {
        objectDensity = Mathf.Clamp(objectDensity - 0.05f, 0.1f, 1f);
    }


   

}
