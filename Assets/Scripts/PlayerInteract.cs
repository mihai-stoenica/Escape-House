using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 10f;
    public GameObject inputMenu;
    public GameObject gridMenu;
    private ChestController currentChest;
    public GridGenerator gridGenerator;

    PlayerController playerController;
    CameraMovment cameraMovment;


    private Transform currentRotatable;
    public float rotationSpeed = 100f;
    
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        cameraMovment = GetComponent<CameraMovment>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
            {
                DoorController door = hit.transform.GetComponent<DoorController>();
                PictureTwist twist = hit.transform.GetComponent<PictureTwist>();
                ChestController chest = hit.transform.GetComponent<ChestController>();
                KeyFragmentsController keyFragmentsController = hit.transform.GetComponent<KeyFragmentsController>();
                GridHindController gridHindController = hit.transform.GetComponent<GridHindController>();

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

                    ToggleMovement();
                    
                    Cursor.visible = true;

                }
                if (keyFragmentsController != null)
                {
                    keyFragmentsController.CollectFragment();
                }
                if (gridHindController != null)
                {
                    gridMenu.SetActive(true);
                    gridGenerator.BuildGrid(13, 5, new Vector2Int(7, 4), new Vector2Int(0, 4));

                    

                    ToggleMovement();

                    Cursor.visible = true;
                }


            }



            if (currentRotatable != null)
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0f)
                {
                    currentRotatable.Rotate(Vector3.up, scroll * rotationSpeed);
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

            ToggleMovement();
        }
        else
        {
            Debug.LogError("No chest to unlock.");
        }
    }

    void ToggleMovement()
    {
        playerController.canMove = !playerController.canMove;
        cameraMovment.canMove = !cameraMovment.canMove;

        Cursor.lockState = (Cursor.lockState == CursorLockMode.None) ?  CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !Cursor.visible;
    }

}
