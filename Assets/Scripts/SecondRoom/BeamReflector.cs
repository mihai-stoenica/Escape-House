using UnityEngine;

public class BeamReflector : MonoBehaviour
{
    private bool isInDevelopment = true;
    public int maxReflections = 6;
    public float maxDistance = 100f;
    private LineRenderer lineRenderer;

    public MirrorPlacer mirrorPlacer;

    private int layerMask;

    private int numberOfMirrors;

    void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Glass"));
        lineRenderer = GetComponent<LineRenderer>();
        numberOfMirrors = mirrorPlacer.numberOfMirrors;
        Debug.Log("Number of mirrors: " + numberOfMirrors);
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

                if (hit.collider.CompareTag("Candle") && (reflections >= numberOfMirrors-1 || isInDevelopment))
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
