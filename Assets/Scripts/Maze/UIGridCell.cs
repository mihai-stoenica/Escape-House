using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
public class UIGridCell : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int coords;
    private static Stack<Vector2Int> path = new Stack<Vector2Int>();
    public bool selected = false;
    private Image image;
    private bool isEndPoint = false;
    private static Vector2Int destination;

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
        if(path.Count == 0)
        {
            Debug.LogError("Path stack is empty. Cannot toggle cell.");
        }
        Vector2Int currentPos = path.Peek();
        if ((!isEndPoint && isNeighbour(currentPos)) || currentPos == coords)
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        if (!selected)
        {
            selected = true;
            image.color = Color.yellow;
            path.Push(coords);
            if (isNeighbour(destination))
            {
                while(path.Count > 0)
                {
                    Debug.Log("Path: " + path.Pop());
                }
                
            }
        }
        else
        {
            Debug.Log("Toggling cell: " + coords);
            Debug.Log("Current path top: " + path.Peek());
            if (path.Peek() == coords)
            {
                selected = false;
                image.color = Color.white;
                path.Pop();
            }
        }
        
    }

    public void MarkStart() 
    { 
        image.color = Color.green;
        isEndPoint = true;
        path.Push(coords);
    }
    public void MarkDestination() 
    {
        isEndPoint = true;
        image.color = Color.red;
        destination = coords;
    }

    public bool isNeighbour(Vector2Int other)
    {
        return (Mathf.Abs(other.x - coords.x) == 1 && other.y == coords.y) ||
               (Mathf.Abs(other.y - coords.y) == 1 && other.x == coords.x);
    }

}
