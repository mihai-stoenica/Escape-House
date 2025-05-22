using UnityEngine;

public class AquariumsManager : MonoBehaviour
{
    public static int noOfAquariums = 4;
    public FloatingController[] aquariums = new FloatingController[noOfAquariums];
    public int[] cuts = new int[noOfAquariums];

    void Start()
    {
        for (int i = 0; i < cuts.Length; i++)
        {
            cuts[i] = -1;
        }
    }

    void Update()
    {
        for (int i = 0; i < noOfAquariums; i++)
        {
            if (aquariums[i].cutEmmiter != cuts[i])
            {
                cuts[i] = aquariums[i].cutEmmiter;
                Debug.Log("Aquarium " + (i + 1) + " cut at emitter: " + aquariums[i].cutEmmiter);
            }
        }
    }
}
