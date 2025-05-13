using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 4f;
    public GameObject cellPrefab;
    public Vector3 mazeOrigin = Vector3.zero;

    private MazeCell[,] grid;

    public Vector2Int start = new Vector2Int(7, 0);
    public Vector2Int destination1 = new Vector2Int(0, 0);
    public Vector2Int destination2 = new Vector2Int(12, 0);

    void Start()
    {
        CreateGrid();
        GenerateMaze(start.x, start.y);
        DeleteEntrances();
        var path1 = ComputePath(grid[destination1.x, destination1.y]);
        var path2 = ComputePath(grid[destination2.x, destination2.y]);

        Debug.Log("Path to destination 1: " + getPathString(path1));
        Debug.Log("Path to destination 2: " + getPathString(path2));
    }

    void CreateGrid()
    {
        grid = new MazeCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = mazeOrigin + new Vector3(x * cellSize, 0, y * cellSize);
                GameObject cellObj = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                grid[x, y] = cellObj.GetComponent<MazeCell>();
            }
        }
    }

    void GenerateMaze(int x, int y)
    {
        MazeCell current = grid[x, y];
        current.visited = true;

        foreach (Vector2Int dir in ShuffleDirections())
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (IsInBounds(nx, ny) && !grid[nx, ny].visited)
            {
                MazeCell next = grid[nx, ny];
                next.previous = current;

                RemoveWallsBetween(current, next, dir);
                GenerateMaze(nx, ny);
            }
        }
    }

    List<Vector2Int> ShuffleDirections()
    {
        var dirs = new List<Vector2Int> {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
        for (int i = 0; i < dirs.Count; i++)
        {
            Vector2Int temp = dirs[i];
            int randIndex = Random.Range(i, dirs.Count);
            dirs[i] = dirs[randIndex];
            dirs[randIndex] = temp;
        }
        return dirs;
    }

    bool IsInBounds(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    void RemoveWallsBetween(MazeCell current, MazeCell next, Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            Destroy(current.wallNorth);
            Destroy(next.wallSouth);
        }
        else if (direction == Vector2Int.down)
        {
            Destroy(current.wallSouth);
            Destroy(next.wallNorth);
        }
        else if (direction == Vector2Int.right)
        {
            Destroy(current.wallEast);
            Destroy(next.wallWest);
        }
        else if (direction == Vector2Int.left)
        {
            Destroy(current.wallWest);
            Destroy(next.wallEast);
        }
    }

    void DeleteEntrances()
    {
        MazeCell startCell = grid[start.x, start.y];
        MazeCell destination1Cell = grid[destination1.x, destination1.y];
        MazeCell destination2Cell = grid[destination2.x, destination2.y];

        startCell.wallSouth.SetActive(false);
        destination1Cell.wallSouth.SetActive(false);
        destination2Cell.wallSouth.SetActive(false);
    }

    List<MazeCell> ComputePath(MazeCell destination)
    {
        List<MazeCell> path = new List<MazeCell>();
        MazeCell current = destination;

        while (current != grid[start.x, start.y])
        {
            path.Add(current);
            current = current.previous;
        }
        path.Add(current);
        path.Reverse();
        return path;
    }

    string getPathString(List<MazeCell> path)
    {
        string pathString = "";
        for (int i = 0; i < path.Count - 1; i++)
        {
            MazeCell source = path[i];
            MazeCell destination = path[i + 1];

            if (source.transform.position.x < destination.transform.position.x)
            {
                pathString += "R";
            }
            else if (source.transform.position.x > destination.transform.position.x)
            {
                pathString += "L";
            }
            else if (source.transform.position.z < destination.transform.position.z)
            {
                pathString += "F";
            }
            else if (source.transform.position.z > destination.transform.position.z)
            {
                pathString += "B";
            }
        }

        return pathString;
    }
}




//-29.85 -6.805, 25.8 + 18.72816,  55.8 + 58.82217