using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    
    [SerializeField]Vector2Int gridsize;
    [SerializeField]int worldGridSize = 10;
    public int UnityGridSize {get {return worldGridSize;}}    
    Dictionary<Vector2Int , Nodes> grid = new Dictionary<Vector2Int, Nodes>(); 
    public Dictionary<Vector2Int , Nodes> Grid {get { return grid; }} 
    

 void Awake() {
         CreateGrid();
        
         
    }

        public Nodes GetNode(Vector2Int coordinates){
        if(grid.ContainsKey(coordinates)){
            return grid[coordinates];
        }
        
        return null;
    }

    public void blockedNode(Vector2Int coordinates){
         if(grid.ContainsKey(coordinates)){
            grid[coordinates].isWalkable = false;
         }
    }


    public void resetNodes(){
        foreach(KeyValuePair<Vector2Int, Nodes> entry in grid){
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;

        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position){
        Vector2Int coordinate = new Vector2Int();
        coordinate.x = Mathf.RoundToInt(position.x / worldGridSize);
        coordinate.y = Mathf.RoundToInt(position.z / worldGridSize);

        return coordinate;
    }  


    public Vector3 GetPOsitionFromCoordinates(Vector2Int coordinate){
        Vector3 position = new Vector3();
        position.x = coordinate.x * worldGridSize;
        position.z = coordinate.y * worldGridSize;

        return position;
    }  

    void CreateGrid(){
         for(int x =0; x<= gridsize.x;x++){
            for(int y = 0;y <= gridsize.y;y++){
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Nodes(coordinates, true));
                // Debug.Log(grid[coordinates].coordinates + " " + grid[coordinates].isWalkable);
            }
         }
    }

    
}
