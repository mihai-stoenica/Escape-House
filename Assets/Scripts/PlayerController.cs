/*using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sprintMultiplier = 2f; 
    public float jumpHeight = 1.5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float gravity = -9.8f;
    private bool isCursorLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            currentSpeed *= sprintMultiplier;
        }

        float translation = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        Vector3 move = transform.forward * translation + transform.right * straffe;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(move + playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isCursorLocked;
        }
    }
}*/

using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sprintMultiplier = 2f;
    public float jumpHeight = 1.5f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float gravity = -10f;
    public float runningSpeed = 0.8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            currentSpeed *= sprintMultiplier;
        }

        float translation = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;

        Vector3 move = transform.forward * translation + transform.right * straffe;
        controller.Move(move);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}