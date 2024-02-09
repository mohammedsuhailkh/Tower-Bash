using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{

    [SerializeField]public bool isClickable = true;
    public bool IsCLickable { get {  return isClickable; }}
    Vector2Int coordinates = new Vector2Int();
    public TowerScript attackers;
    NodeManager nodeManager;
    PathFinder pathFinder;


  
    // Start is called before the first frame update
    private void Awake() {
      nodeManager = FindObjectOfType<NodeManager>();
     pathFinder = FindObjectOfType<PathFinder>(); 
    }

    private void Start() {
      if(nodeManager != null){
        coordinates = nodeManager.GetCoordinatesFromPosition(transform.position);
        if(!isClickable){
          nodeManager.blockedNode(coordinates);
        }
      }
    }
private void OnMouseDown() {  

 

    if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
          if(nodeManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates)){
            Vector3 tPosition = new Vector3(transform.position.x, attackers.transform.position.y, transform.position.z);
            bool isSuccessfull = attackers.CreateTower(attackers, tPosition);
            
            if(isSuccessfull){
              nodeManager.blockedNode(coordinates);
              pathFinder.NotifyRecievers();
            }
            
          
        }
    
  }

  }
}

