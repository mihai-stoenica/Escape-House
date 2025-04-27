using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*public static UIManager Instance;

    public GameObject inputPanel; 
    public InputField codeInputField;
    private Chest currentChest;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenInputMenu(Chest chest)
    {
        inputPanel.SetActive(true);
        codeInputField.text = "";
        currentChest = chest;
    }

    public void SubmitCode()
    {
        if (currentChest != null)
        {
            int enteredCode;
            if (int.TryParse(codeInputField.text, out enteredCode))
            {
                currentChest.TryUnlock(enteredCode);
            }
        }
        inputPanel.SetActive(false);
    }

    public void CloseInputMenu()
    {
        inputPanel.SetActive(false);
    }*/
}
