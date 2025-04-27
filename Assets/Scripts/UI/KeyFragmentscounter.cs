using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyFragmentCounter : MonoBehaviour
{
    public int totalFragments = 6;
    private int collectedFragments = 0;
    public Animator frontDoorAnimator;

    public TextMeshProUGUI keyCounterText;

    void Start()
    {
        if(keyCounterText == null)
        {
            Debug.LogError("Key Counter Text is not assigned in the inspector.");
            return;
        }
        UpdateUI();
    }

    public void CollectFragment()
    {
        collectedFragments++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        keyCounterText.text = "Collected Fragments: " + $"{collectedFragments}/{totalFragments}";
        if(collectedFragments == totalFragments)
        {
            frontDoorAnimator.SetTrigger("OpenDoor");
        }
    }
}
