using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator animator;
    private bool isUnlocked = false;
    public int unlockCode; 

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator missing on " + gameObject.name);
        }
    }

    public void ToggleChest()
    {
        animator.SetTrigger("ChestOpen");
        isUnlocked = true;
    }

    public void UnlockChest(int enteredCode)
    {
        if (isUnlocked)
        {
            Debug.LogError("Chest already locked");
        }

        if (enteredCode == unlockCode)
        {
            //Debug.Log("Chest unlocked!");
            ToggleChest();
        }
        else
        {
            Debug.Log("Wrong code!");
        }
    }
}