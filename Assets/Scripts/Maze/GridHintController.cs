using UnityEngine;
using System.Collections.Generic;
using System;
public class GridHintController : MonoBehaviour
{

    private Stack<Vector2Int> path1 = new Stack<Vector2Int>();

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePath(Vector2Int destination)
    {
        path1 = UIGridCell.path;
        string reconstructedPath = ReconstructPath(path1, destination);
        char[] reversedRecosntructedPath = reconstructedPath.ToCharArray();
        Array.Reverse(reversedRecosntructedPath);
        string reversed = new string(reversedRecosntructedPath);
        Debug.Log("Reconstructed path: " + reversed);
    } 

    private string ReconstructPath(Stack<Vector2Int> path, Vector2Int destination)
    {
        string reconstructedPath = "";

        Vector2Int current = destination;

        while (path.Count > 0)
        {
            Vector2Int previous = path.Pop();
            if(previous.x - current.x == -1 && previous.y == current.y)
            {
                reconstructedPath += "R";
                current = previous;
            }
            else if(previous.x - current.x == 1 && previous.y == current.y)
            {
                reconstructedPath += "L";
                current = previous;
            }
            else if (previous.y - current.y == 1 && previous.x == current.x)
            {
                reconstructedPath += "F";
                current = previous;
            }
            else if (previous.y - current.y == -1 && previous.x == current.x)
            {
                reconstructedPath += "B";
                current = previous;
            }
        }
        return reconstructedPath;
    }
   
}
