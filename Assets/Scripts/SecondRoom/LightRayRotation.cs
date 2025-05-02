using UnityEngine;

public class LightRayRotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
    }
}
