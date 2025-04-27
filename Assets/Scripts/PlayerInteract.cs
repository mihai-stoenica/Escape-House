using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 10f;
    public GameObject inputMenu;
    private ChestController currentChest;

    PlayerController playerController;
    CameraMovment cameraMovment;
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        cameraMovment = GetComponent<CameraMovment>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
            {
                DoorController door = hit.transform.GetComponent<DoorController>();
                PictureTwist twist = hit.transform.GetComponent<PictureTwist>();
                ChestController chest = hit.transform.GetComponent<ChestController>();
                KeyFragmentsController keyFragmentsController = hit.transform.GetComponent<KeyFragmentsController>();
                
                if (door != null)
                {
                    door.ToggleDoor();
                }
                if (twist != null)
                {
                    twist.TogglePicture();
                }
                if (chest != null)
                {
                    currentChest = chest;
                    inputMenu.SetActive(true);

                    
                    playerController.canMove = false;
                    cameraMovment.canMove = false;

                    Cursor.lockState = CursorLockMode.None; 
                    Cursor.visible = true;
                }
                if (keyFragmentsController != null)
                {
                    keyFragmentsController.CollectFragment();
                }
            }
        }
    }

    public void SubmitChestCode()
    {
        TMP_InputField inputField = inputMenu.GetComponentInChildren<TMP_InputField>();
        if (inputField == null)
        {
            Debug.LogError("TMP_InputField not found in inputMenu");
            return;
        }
        string codeText = inputField.text;
        if (currentChest != null)
        {
            int enteredCode;
            if (int.TryParse(codeText, out enteredCode))
            {
                Debug.Log("Entered code: " + enteredCode);
                currentChest.UnlockChest(enteredCode);
            }
            else
            {
                Debug.Log("Invalid input, not a number.");
            }

            inputMenu.SetActive(false);
            currentChest = null;
            playerController.canMove = true;
            cameraMovment.canMove = true;
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;                   
            

        }
        else
        {
            Debug.LogError("No chest to unlock.");
        }
    }
}
