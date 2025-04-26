using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    public float pickupRange = 5.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            if (hit.transform.CompareTag("Pickup"))
            {
                PickupObject(hit.transform.gameObject);
            }
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            heldObj = pickObj;
            heldObjRb = rb;

            heldObjRb.useGravity = false;
            heldObjRb.isKinematic = true;

            heldObj.transform.SetParent(holdArea);
            heldObj.transform.localPosition = Vector3.zero;
            heldObj.transform.localRotation = Quaternion.identity;
        }
    }

    void DropObject()
    {
        if (heldObj != null)
        {
            heldObj.transform.SetParent(null);

            Vector3 dropPosition = holdArea.position + transform.forward * 0.8f + Vector3.up * 0.5f;
            heldObj.transform.position = dropPosition;

            heldObjRb.isKinematic = false;
            heldObjRb.useGravity = true;

            heldObjRb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            heldObj = null;
            heldObjRb = null;
        }
    }

}
