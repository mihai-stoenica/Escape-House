using UnityEngine;

public class BeamReflector : MonoBehaviour
{
    public int maxReflections = 6;
    public float maxDistance = 100f;
    private LineRenderer lineRenderer;

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

                if (hit.collider.CompareTag("Candle") && reflections >= maxReflections / 2 + 1)
                {
                    CandleController reaction = hit.collider.GetComponentInParent<CandleController>();
                    if (reaction != null)
                    {
                        reaction.BeamHit();
                    }
                }


                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point;
                    reflections++;
                }
                else
                {
                    break;
                }

            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(reflections + 1, position + direction * maxDistance);
                break;
            }
        }
    }
}
