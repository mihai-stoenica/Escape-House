using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightEmmiter : MonoBehaviour
{
    public Transform target; 
    private LineRenderer line;
    public LayerMask obstacleMask;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float maxDistance = Vector3.Distance(transform.position, target.position);

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance, obstacleMask))
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point); 
        }
        else
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, target.position);
        }
    }
}
