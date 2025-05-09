using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform gridParent;
    private UIGridCell[,] uiCells;


    
    public void BuildGrid(int width, int height, Vector2Int start, Vector2Int destination)
    {
        uiCells = new UIGridCell[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                UIGridCell cellScript = cell.GetComponent<UIGridCell>();
                cellScript.coords = new Vector2Int(x, y);
                uiCells[x, y] = cellScript;
                /*Debug.Log($"Cell created at {x}, {y}");
                if (cell.GetComponent<Image>() != null)
                {
                    Debug.Log("Image component found.");
                }
                else
                {
                    Debug.LogError("Image component is missing after instantiation!");
                }*/

            }
        }
        
        StartCoroutine(MarkStartAfterInstantiation(start, destination));

        //uiCells[start.x, start.y].MarkStart();
        //uiCells[destination.x, destination.y].MarkDestination();
    }

    private IEnumerator MarkStartAfterInstantiation(Vector2Int start, Vector2Int destination)
    {
        yield return null;

        uiCells[start.x, start.y].MarkStart();
        uiCells[destination.x, destination.y].MarkDestination();
    }


    public List<Vector2Int> GetUserPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();
        foreach (var cell in uiCells)
        {
            if (cell.selected)
                path.Add(cell.coords);
        }
        return path;
    }

    public void ResetGrid()
    {
        foreach (var cell in uiCells)
            cell.ResetCell();
    }
}
