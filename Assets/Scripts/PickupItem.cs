using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public bool isHolding = false;
    public float throwForce = 600f;
    public float maxDistance = 3f;
    public float distance;

    PickupHolder holder;
    Rigidbody rb;
    Vector3 objectPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        holder = PickupHolder.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            Hold();
        }
        
    }

    private void OnMouseDown()
    {
        if(holder != null)
        {
            distance = Vector3.Distance(this.transform.position, holder.transform.position);
            if (distance <= maxDistance)
            {
                this.transform.SetParent(holder.transform);
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;

                
            }
        }
        else
        {
            Debug.Log("Holder not found");
        }
    }

    private void OnMouseUp()
    {
        Drop();
    }

   

    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, holder.transform.position);
        if (distance >= maxDistance)
        {
            Drop();
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButtonDown(1))
        {
            //throw
        }
    }

    private void Drop()
    {
        if(isHolding)
        {
            this.transform.SetParent(null);
            isHolding =false;
            objectPos = this.transform.position;    
            this.transform.position = objectPos;  
            rb.useGravity = true;
            
            
        }
    }
}

