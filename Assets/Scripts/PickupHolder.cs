using UnityEngine;

public class PickupHolder : MonoBehaviour
{
    public static PickupHolder Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
