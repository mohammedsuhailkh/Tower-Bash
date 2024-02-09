using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField]Vector2Int startCoordinate;
    public Vector2Int StartCoordinate {get {return startCoordinate;}}
    [SerializeField]Vector2Int destinationCoordinate;
    public Vector2Int DestinationCoordinate {get {return destinationCoordinate;}}

    
    Nodes startNode;
    Nodes destinationNode;
    [SerializeField]Nodes currentSearchNode;
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    NodeManager nodeManager;
    Dictionary<Vector2Int, Nodes> grid = new Dictionary<Vector2Int, Nodes>();
    Dictionary<Vector2Int, Nodes> reached = new Dictionary<Vector2Int, Nodes>();

    Queue<Nodes> frontier = new Queue<Nodes>();
    // Start is called before the first frame update
    void Awake()
    {
        nodeManager = FindObjectOfType<NodeManager>(); 
       if(nodeManager!= null){
        grid = nodeManager.Grid;
        startNode = nodeManager.Grid[startCoordinate];
        destinationNode = nodeManager.Grid[destinationCoordinate];
        
       }

        
    }

    void Start()
    {
         
        GetNewPath();
    }

    public List<Nodes> GetNewPath(){
        nodeManager.resetNodes();
        BreadthFirstSearch();
        return Buildpath();
    }



        void ExploreNeighbors(){
        List<Nodes> neighbors = new List<Nodes>();
        foreach(Vector2Int direction in directions){
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoords)){
                neighbors.Add(grid[neighborCoords]);

               
            }
        }

         foreach(Nodes neighbor in neighbors){
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }

        }


        void BreadthFirstSearch(){
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();


         bool isRunning = true;
         frontier.Enqueue(startNode);
         reached.Add(startCoordinate,startNode);

         while (frontier.Count > 0 && isRunning)
    {
        currentSearchNode = frontier.Dequeue();
        currentSearchNode.isExplored = true;

        // Check if the current node is the destination node
        if (currentSearchNode == destinationNode)
        {
            isRunning = false;
        }

        ExploreNeighbors();
    }

         
    }



    List<Nodes> Buildpath()
{
    List<Nodes> paths = new List<Nodes>();
    Nodes currentNode = destinationNode;

    paths.Add(currentNode);
    currentNode.isPath = true;

    while (currentNode.connectedTo != null)
    {
        currentNode = currentNode.connectedTo;
        paths.Add(currentNode);
        currentNode.isPath = true;
    }

    paths.Reverse();
       Debug.Log("Path Nodes:");
    foreach (Nodes node in paths)
    {
        Debug.Log("Node Coordinates: " + node.coordinates);
        // Add more information if needed
    }

    return paths;
}

public bool WillBlockPath(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Nodes> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1){
                GetNewPath();
                return true;
            }

        }
        return false;
    }

public void NotifyRecievers()
{
    // Broadcast a message to all active enemies that a new path has been generated
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Castle");
    foreach (GameObject enemy in enemies)
    {
        enemy.SendMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}


}

