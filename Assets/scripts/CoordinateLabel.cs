using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CoordinateLabel : MonoBehaviour
{
    TMP_Text tMP_Text;
    Vector2Int coordinate = new Vector2Int();
   
    [SerializeField]Color defaultColor = Color.white;
    [SerializeField]Color blockedColor = Color.blue;
    [SerializeField]Color exploredColor = Color.yellow;
    [SerializeField]Color pathColor = Color.red;
    NodeManager nodeManager;
    

    // Start is called before the first frame update
    private void Start()
    {
        tMP_Text = GetComponent<TMP_Text>();
        nodeManager = FindObjectOfType<NodeManager>();
        tMP_Text.enabled = false;
        //  tMP_Text.text = "haii";
        displayCoordinates();
        UpdateObjectName();
       
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            displayCoordinates();
            UpdateObjectName();
        }
        displayCoordinates();
        UpdateObjectName();
        changeColor();
        ToggleText();
       

        
    }
void ToggleText()
{
    if (Input.GetKey(KeyCode.C))
    {
        tMP_Text.enabled = !tMP_Text.isActiveAndEnabled;
    }
}

void changeColor()
{

    if(nodeManager == null){ return; }
   
    Nodes nodes = nodeManager.GetNode(coordinate);
        if(nodes == null){return;}
    if(!nodes.isWalkable){
        tMP_Text.color = blockedColor;
    }else if(nodes.isPath){
        tMP_Text.color = pathColor;
    }else if(nodes.isExplored){
        tMP_Text.color = exploredColor;
    }else{
        tMP_Text.color = defaultColor;
    }
   

}


    void displayCoordinates()
    {
        
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z / 10);

        // Debug.Log(coordinate.x + " , " + coordinate.y);
        tMP_Text.text = coordinate.x + " , " + coordinate.y;
       
    }

       void UpdateObjectName()
    {
        Transform immediateParent = transform.parent;

        if (immediateParent != null)
        {
            Transform grandparent = immediateParent.parent;

            if (grandparent != null)
            {
               
                grandparent.name = coordinate.ToString();
            }
        }
    }

}
