using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
public class UIGridCell : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int coords;
    //private static Vector2Int[] path;
    private static Stack<Vector2Int> path = new Stack<Vector2Int>();
    public bool selected = false;
    private Image image;
    private bool isEndPoint = false;
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
        if (!isEndPoint && isNeighbour(previous))
        {
            Toggle();
        }
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !isEndPoint)
        {
            Toggle();
        }
            
    }*/

    private void Toggle()
    {
        selected = !selected;
        image.color = selected ? Color.yellow : Color.white;
    }

    public void MarkStart() 
    { 
        image.color = Color.green; 
        isEndPoint = true;
    }
    public void MarkDestination() 
    {
        isEndPoint = true;
        image.color = Color.red;
    }

    public bool isNeighbour(Vector2Int other)
    {
        if(other == null || Math.Abs(other.x - coords.x) > 1 || Math.Abs(other.y - coords.y) > 1)
        {
            return false;
        }
        return true;
    }
}
