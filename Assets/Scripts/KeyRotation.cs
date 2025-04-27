using UnityEngine;

public class KeyRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 90, 0); // Degrees per second

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
