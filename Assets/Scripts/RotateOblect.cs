using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float interactRange = 5f;
    public float rotationSpeed = 100f;
    private Transform currentRotatable;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            if (hit.transform.CompareTag("Rotable") || hit.transform.CompareTag("Mirror"))
            {
                currentRotatable = hit.transform;
            }
            else
            {
                currentRotatable = null;
            }
        }
        else
        {
            currentRotatable = null;
        }

        if (currentRotatable != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                currentRotatable.Rotate(Vector3.up, scroll * rotationSpeed);
            }
        }
    }
}
