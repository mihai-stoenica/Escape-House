using UnityEngine;

public class MirrorPlacer : MonoBehaviour
{
    public GameObject mirrorPrefab;
    public int numberOfMirrors;
    public float xMax, xMin, zMax, zMin, y;
    public float collideRadius = 3.5f;
    public int maxAttempts = 100;


    void Start()
    {
        int placedMirrors = 0;
        int attempts = 0;
        while (placedMirrors < numberOfMirrors && attempts < maxAttempts)
        {
            attempts++;
            //Debug.Log(attempts);
            Vector3 randomPos =  GetRandomPosition();
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            Collider[] hits = Physics.OverlapSphere(randomPos, collideRadius);

            bool spaceIsClear = true;
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Mirror") )
                {
                    spaceIsClear = false;
                    break;
                }
            }

            if (spaceIsClear)
            {
                GameObject mirror = Instantiate(mirrorPrefab, randomPos, randomRotation);
                mirror.tag = "Mirror";
                placedMirrors++;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(xMin,xMax),
            y,
            Random.Range(zMin,zMax)
            );
    }


}
