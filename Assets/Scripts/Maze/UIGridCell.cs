using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIGridCell : MonoBehaviour
{
    public Vector2Int coords;
    public bool selected = false;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if(image == null)
        {
            Debug.LogError("Image component not found on UIGridCell.");
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
            Toggle();
    }

    private void Toggle()
    {
        selected = !selected;
        image.color = selected ? Color.yellow : Color.white;
    }

    public void ResetCell()
    {
        selected = false;
        image.color = Color.white;
    }

    public void MarkStart() 
    { 
        image.color = Color.green; 
    }
    public void MarkDestination() 
    { 
        image.color = Color.red;
    }
}
