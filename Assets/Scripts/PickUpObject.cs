using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    public float pickupRange = 5.0f;
    public float pickupForce = 150.0f;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(heldObj == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange)) 
                {
                    pickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                dropObject();
            }
        }
        if (heldObj != null) 
        {
            moveObj();
        }
    }

    void pickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.linearDamping = 10;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRb.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void dropObject()
    {
        heldObjRb.useGravity = true;
        heldObjRb.linearDamping = 1;
        heldObjRb.constraints = RigidbodyConstraints.None;

        heldObjRb.transform.parent = null;
        heldObj = null;
    }

    void moveObj()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRb.AddForce(moveDirection * pickupForce);
        }
    }
}