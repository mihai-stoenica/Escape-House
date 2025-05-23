using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }
    }

    public void ToggleDoor()
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
    }
}
