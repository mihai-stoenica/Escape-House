using UnityEngine;

public class BeamReflector : MonoBehaviour
{
    public int maxReflections = 5;
    public float maxDistance = 100f;
    private LineRenderer lineRenderer;

    // Create a layer mask that ignores the "Glass" layer
    private int layerMask;

    void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Glass"));
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 direction = transform.forward;
        Vector3 position = transform.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, position);

        int reflections = 0;

        while (reflections < maxReflections)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit, maxDistance, layerMask))
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(reflections + 1, hit.point);

                // Check for mirror
                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                    reflections++;
                }
                else
                {
                    // Hit something that's not a mirror — end
                    break;
                }
            }
            else
            {
                // If ray goes off into space
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(reflections + 1, position + direction * maxDistance);
                break;
            }
        }
    }
}
