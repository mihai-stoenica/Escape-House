using UnityEngine;

public class KeyFragmentsController : MonoBehaviour
{
    public GameObject gameManager;
    private KeyFragmentCounter keyFragmentCounter;
    
    void Start()
    {
        keyFragmentCounter = gameManager.GetComponent<KeyFragmentCounter>();
    }

    public void CollectFragment()
    {
        keyFragmentCounter.CollectFragment();
        Destroy(gameObject);
    }
}
