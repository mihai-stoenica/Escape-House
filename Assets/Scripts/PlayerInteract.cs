using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 10f;
    public GameObject inputMenu;
    public GameObject gridMenu;
    public GameObject gridMenu2;
    private ChestController currentChest;
    public GridGenerator gridGenerator;
    public GridGenerator2 gridGenerator2;

    PlayerController playerController;
    CameraMovment cameraMovment;


    private Transform currentRotatable;
    public float rotationSpeed = 100f;
    
    private static int waterLayer;
    private static int layerMask;
    private static int glassLayer;

    void Awake()
    {
        waterLayer = LayerMask.NameToLayer("Water");
        glassLayer = LayerMask.NameToLayer("Glass");
        layerMask = ~((1 << waterLayer) | (1 << glassLayer));
    }

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        cameraMovment = GetComponent<CameraMovment>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inputMenu.activeSelf)
            {
                inputMenu.SetActive(false);
                currentChest = null;
                ToggleMovement();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }
            if (gridMenu.activeSelf)
            {
                gridMenu.SetActive(false);
                ToggleMovement();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }
            if (gridMenu2.activeSelf)
            {
                gridMenu2.SetActive(false);
                ToggleMovement();

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }
        }

        

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerMask))
            {
                DoorController door = hit.transform.GetComponent<DoorController>();
                PictureTwist twist = hit.transform.GetComponent<PictureTwist>();
                ChestController chest = hit.transform.GetComponent<ChestController>();
                KeyFragmentsController keyFragmentsController = hit.transform.GetComponent<KeyFragmentsController>();
                GridHintController gridHintController = hit.transform.GetComponent<GridHintController>();
                FloatingController floatingController = hit.transform.GetComponent<FloatingController>();

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

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                if (gridHintController != null )
                {
                    if(gridHintController.name == "Destination1Grid" && !gridMenu.activeSelf)
                    {
                        gridMenu.SetActive(true);

                        if (!gridGenerator.isBuilt)
                        {
                            gridGenerator.BuildGrid(13, 6, new Vector2Int(7, 5), new Vector2Int(12, 5));
                        }

                        ToggleMovement();

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    else if(gridHintController.name == "Destination2Grid" && !gridMenu2.activeSelf)
                    {
                        gridMenu2.SetActive(true);

                        if (!gridGenerator2.isBuilt)
                        {
                            gridGenerator2.BuildGrid(13, 6, new Vector2Int(7, 5), new Vector2Int(0, 5));
                        }

                        ToggleMovement();

                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }

                }

                if(floatingController != null)
                {
                    Debug.Log("FloatingController found");
                    if (Input.GetKey(KeyCode.H))
                    {
                        float scroll = Input.GetAxis("Mouse ScrollWheel");

                        if (scroll > 0f)
                        {
                            floatingController.DecreaseDensity();
                        }
                        else if (scroll < 0f)
                        {
                            floatingController.IncreaseDensity();
                        }
                    }
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
    }

}
