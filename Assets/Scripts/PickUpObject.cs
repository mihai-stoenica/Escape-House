using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform pickupPoint; // Empty GameObject 1m in front of camera
    private Camera cam;
    private GameObject heldObject;
    private bool isHolding = false;
    private LayerMask pickupLayer;

    void Start()
    {
        cam = GetComponent<Camera>();
        pickupLayer = LayerMask.GetMask("Default"); // Pickup objects layer
    }

    void Update()
    {
        // Raycast from crosshair
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Pick up
        if (Input.GetMouseButtonDown(0) && !isHolding)
        {
            if (Physics.Raycast(ray, out hit, 5f, pickupLayer))
            {
                if (hit.collider.CompareTag("Pickup"))
                {
                    heldObject = hit.collider.gameObject;
                    isHolding = true;
                    // Parent to pickupPoint, reset local transform
                    heldObject.transform.SetParent(pickupPoint);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.transform.localRotation = Quaternion.identity;
                    // Disable physics
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.isKinematic = true;
                        rb.useGravity = false;
                    }
                    Collider col = heldObject.GetComponent<Collider>();
                    if (col) col.enabled = false;
                    Debug.Log("Picked up: " + heldObject.name);
                }
            }
        }

        // Release
        if (Input.GetMouseButtonUp(0) && isHolding)
        {
            isHolding = false;
            heldObject.transform.SetParent(null);
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            Collider col = heldObject.GetComponent<Collider>();
            if (col) col.enabled = true;
            heldObject = null;
            Debug.Log("Released object");
        }
    }
}