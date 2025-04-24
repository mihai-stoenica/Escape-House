using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInTrigger = false;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name + ", Tag: " + other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Debug.Log("Player entered trigger on " + gameObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            Debug.Log("Player exited trigger on " + gameObject.name);
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                animator.SetTrigger("CloseDoor");
                isOpen = false;
            }
            else
            {
                animator.SetTrigger("OpenDoor");
                isOpen = true;
            }
            Debug.Log("E pressed, isOpen: " + isOpen + " on " + gameObject.name);
        }
    }
}