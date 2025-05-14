using UnityEngine;
using System.Collections.Generic;
using System;
public class GridHintController : MonoBehaviour
{

    private Stack<Vector2Int> path1 = new Stack<Vector2Int>();
    private Stack<Vector2Int> path2 = new Stack<Vector2Int>();

    public MazeGenerator mazeGenerator;
    private string correctPath1;
    private string correctPath2;

    private bool isCorrectPath1 = false;
    private bool isCorrectPath2 = false;

    public ChestController ChestController;

    void Start()
    {
        Debug.Log("GridHintController Start called, mazeGenerator: " + mazeGenerator);
        correctPath1 = mazeGenerator.stringPath1;
        correctPath2 = mazeGenerator.stringPath2;
        if(!ChestController)
        {
            Debug.LogError("ChestController not found in GridHintController.");
        }
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePath1(Vector2Int destination)
    {
        path1 = UIGridCell.path;
        string reconstructedPath = ReconstructPath(path1, destination);
        char[] reversedRecosntructedPath = reconstructedPath.ToCharArray();
        Array.Reverse(reversedRecosntructedPath);
        string reversed = new string(reversedRecosntructedPath);
        Debug.Log("Reconstructed path1: " + reversed);
        if(correctPath1 == reversed)
        {
            Debug.Log("Correct path1");
            isCorrectPath1 = true;
            if(isCorrectPath2 == true)
            {
                Debug.Log("Chest opened");
                ChestController.ToggleChest();
            }
        }
        else
        {
            Debug.Log("Incorrect path1");
        }
    }

    public void UpdatePath2(Vector2Int destination)
    {
        path2 = UIGridCell2.path;
        string reconstructedPath = ReconstructPath(path2, destination);
        char[] reversedRecosntructedPath = reconstructedPath.ToCharArray();
        Array.Reverse(reversedRecosntructedPath);
        string reversed = new string(reversedRecosntructedPath);
        Debug.Log("Reconstructed path2: " + reversed);
        if (correctPath2 == reversed)
        {
            Debug.Log("Correct path2");
            isCorrectPath2 = true;
            if(isCorrectPath1 == true)
            {
                Debug.Log("Chest opened");
                ChestController.ToggleChest();
            }
        }
        else
        {
            Debug.Log("Incorrect path2");
        }
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
