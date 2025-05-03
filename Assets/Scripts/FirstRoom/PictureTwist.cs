using UnityEngine;

public class PictureTwist : MonoBehaviour
{
    private Animator animator;
    private bool isTwisted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }
    }

    public void TogglePicture()
    {
        if (isTwisted)
        {
            animator.SetTrigger("OpenPicture");
            isTwisted = false;
        }
        else
        {
            animator.SetTrigger("ClosePicture");
            isTwisted = true;
        }
    }
}
