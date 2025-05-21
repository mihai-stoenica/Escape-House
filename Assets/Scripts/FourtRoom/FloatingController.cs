using UnityEngine;

public class FloatingController : MonoBehaviour
{
    public float floatStrength = 10f;
    public float objectDensity = 1f;

    private Rigidbody rb;
    private float waterTop;
    private float waterBottom;
    private bool inWater = false;
    private float waterDrag = 1f;

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
        objectDensity = Mathf.Clamp(objectDensity + 0.1f, 0.1f, 1f);
    }

    public void DecreaseDensity()
    {
        objectDensity = Mathf.Clamp(objectDensity - 0.1f, 0.1f, 1f);
    }


   

}
