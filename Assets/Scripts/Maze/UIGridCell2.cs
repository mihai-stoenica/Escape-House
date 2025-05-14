using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
public class UIGridCell2 : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int coords;
    public static Stack<Vector2Int> path = new Stack<Vector2Int>();
    public bool selected = false;
    private Image image;
    private bool isStart = false;
    private static Vector2Int destination;

    public GridHintController gridController; 

    private void Awake()
    {
        image = GetComponent<Image>();
        if(image == null)
        {
            Debug.LogError("Image component not found on UIGridCell.");
            return;
        }
        gridController = FindFirstObjectByType<GridHintController>();
        if (gridController == null)
        {
            Debug.LogError("grid controller component not found on UIGridCell.");
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
        if (!isStart && (isNeighbour(currentPos) || currentPos == coords))
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
            if (coords == destination)
            {
                //while(path.Count > 0)
                //{

                //Debug.Log("Popping path: " + path.Pop());
                //}
                gridController.UpdatePath2(destination);
                //}
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
        isStart = true;
        image.color = Color.green;
        
        path.Push(coords);
    }
    public void MarkDestination() 
    {
        image.color = Color.red;
        destination = coords;
    }

    public bool isNeighbour(Vector2Int other)
    {
        return (Mathf.Abs(other.x - coords.x) == 1 && other.y == coords.y) ||
               (Mathf.Abs(other.y - coords.y) == 1 && other.x == coords.x);
    }

}
