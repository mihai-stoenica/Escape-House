using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 10f;
   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
            {
                DoorController door = hit.transform.GetComponent<DoorController>();
                PictureTwist twist = hit.transform.GetComponent<PictureTwist>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
                if (twist != null)
                {
                    twist.TogglePicture();
                }
            }
        }
    }
}
