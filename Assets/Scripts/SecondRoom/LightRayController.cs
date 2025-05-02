using UnityEngine;
using UnityEngine.UIElements;

public class LightRayController : MonoBehaviour
{
    private Vector3 position;
    public float rotationSpeed = 45f;


    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
