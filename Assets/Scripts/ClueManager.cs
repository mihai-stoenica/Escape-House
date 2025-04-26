using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public int cluesNumber;
    public GameObject[] clues;

    
    void Start()
    {
        HideAllClues();
        RevealOneClue();
    }

    private void HideAllClues()
    {
        foreach (GameObject clue in clues) {
            if (clue != null) {
                clue.SetActive(false);
            }
        }
    }

    private void RevealOneClue()
    {
        if (clues.Length == 0)
        {
            Debug.LogWarning("No clues assigned");
            return;
        }

        int randomIndex = Random.Range(0, cluesNumber);
        if (clues[randomIndex] != null)
            clues[randomIndex].SetActive(true);
    }
}
