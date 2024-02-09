using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMover : MonoBehaviour
{
    List<Nodes> path = new List<Nodes>();
    [SerializeField][Range(0f,5f)] float speed = 1f;
    float posY = 3.555f;
    Enemyscript enemyscript;
    NodeManager nodeManager;
    PathFinder pathFinder;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        
        
    }
    private void Awake() {
        enemyscript = GetComponent<Enemyscript>();
        nodeManager = FindObjectOfType<NodeManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

   void ReturnToStart()
{
    Vector3 startPosition = transform.position;
    Vector3 endPosition = nodeManager.GetPOsitionFromCoordinates(pathFinder.StartCoordinate);
    endPosition.y = posY;  // Set the Y-axis position
    transform.position = endPosition;
}

    void RecalculatePath(){
        path.Clear(); 
        path = pathFinder.GetNewPath();
        
    }

    void Finishpath(){
        enemyscript.PenaltyGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath(){
        for(int i = 0;i<path.Count;i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = nodeManager.GetPOsitionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPosition);
        
            while(travelPercent < 1){
                travelPercent += Time.deltaTime * speed;
    
                transform.position = Vector3.Lerp(startPosition,endPosition,travelPercent); 
                yield return new WaitForEndOfFrame();
            }
        }
        Finishpath();
        
    }

   

    
}
