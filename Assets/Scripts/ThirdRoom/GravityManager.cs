using UnityEngine;
using TMPro;

public class GravityManager : MonoBehaviour
{
    public float gravityScale = 1.0f;
    public TextMeshProUGUI gravityUI;

    void Start()
    {
        gravityUI.text = "Gravity Scale: " + gravityScale.ToString("F1") + "x";
    }

    public void IncreaseGravity()
    {
        gravityScale += 0.1f;
        gravityScale = Mathf.Clamp(gravityScale, 0.1f, 10f);
        gravityUI.text = "Gravity Scale: " + gravityScale.ToString("F1") + "x";
        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);
    }

    public void DecreaseGravity()
    {
        gravityScale -= 0.1f;
        if (gravityScale < 0.1f)
        {
            gravityScale = 0.1f;
        }
        gravityScale = Mathf.Clamp(gravityScale, 0.1f, 10f);
        Physics.gravity = new Vector3(0, -9.81f * gravityScale, 0);
        gravityUI.text = "Gravity Scale: " + gravityScale.ToString("F1") + "x";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0f)
            {
                IncreaseGravity();
            }
            else if (scroll < 0f)
            {
                DecreaseGravity();
            }
        }
    }
}
