using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public GameObject wallNorth;
    public GameObject wallSouth;
    public GameObject wallEast;
    public GameObject wallWest;

    [HideInInspector]
    public bool visited = false;
    [HideInInspector] 
    public MazeCell previous;
}
