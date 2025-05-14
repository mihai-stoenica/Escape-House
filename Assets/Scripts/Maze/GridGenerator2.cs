using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GridGenerator2 : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform gridParent;
    private UIGridCell2[,] uiCells;
    public bool isBuilt = false;

    public void BuildGrid(int width, int height, Vector2Int start, Vector2Int destination)
    {
        uiCells = new UIGridCell2[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                UIGridCell2 cellScript = cell.GetComponent<UIGridCell2>();
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
        isBuilt = true;
        
    }

    private IEnumerator MarkStartAfterInstantiation(Vector2Int start, Vector2Int destination)
    {
        yield return null;

        uiCells[start.x, start.y].MarkStart();
        uiCells[destination.x, destination.y].MarkDestination();
    }

  
}
