using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	List<Node> canMoveNodes;
	PathFinding pathfinding;

	public GameObject container; 
	public GameObject movementPrefabContainer;
	public GameObject canMovePrefab;
	public GameObject atackPrefab;

	public GameObject mapGO;
	public Map mapComp;

	public Node actualNode;
	public Node goalNode;


	public GameObject mouseManagerGO;
	public MouseManager mouseManagerComp;


	public GameObject prevSelectedGO;
	public GameObject selectedGO;
	public GameObject goalTileGo;


	// Use this for initialization
	void Start () {

		canMoveNodes = new List<Node>();
		pathfinding = new PathFinding ();


		mouseManagerGO = GameObject.FindObjectOfType<MouseManager> ().gameObject;
		mouseManagerComp = mouseManagerGO.GetComponent<MouseManager> ();

		mapGO = GameObject.FindObjectOfType<Map> ().gameObject;
		mapComp = mapGO.GetComponent<Map> ();



	}
	
	// Update is called once per frame
	void Update ()
    {
        CreateCanMoveTiles();
       
	}

    
    public void MoveToDestination(GameObject destination)
    {
        selectedGO.transform.position = destination.transform.position;
    }
    
    public void CreateCanMoveTiles()
    {
        selectedGO = mouseManagerComp.selectedUnit;

        if (container != null)
        {
            //Como el previo se crea despues de revisar este "if"
            //si es que cambia el selected Go no sera igual al previo
            if (selectedGO != prevSelectedGO)
            {
                Destroy(container);

            }
        }


        if (selectedGO != null)
        {
            actualNode = mapComp.GetNode(Mathf.FloorToInt(selectedGO.transform.position.x), Mathf.FloorToInt(selectedGO.transform.position.y));

            // Acá creo el previo del selected se crea
            prevSelectedGO = selectedGO;

            if (container == null)
            {
                container = Instantiate(movementPrefabContainer, new Vector3(), Quaternion.identity);

                //Aca trato de crear 
                canMoveNodes = pathfinding.CreateCanMoveToTiles(mapComp, actualNode, 5);



                //instantiate can move prefabs to the x, y coordinates of the nodes


                foreach (Node node in canMoveNodes)
                {
                    int x = node.x;
                    int y = node.y;
                    Instantiate(canMovePrefab, new Vector3(x, y, -0.5f), Quaternion.identity, container.transform);
                }


            }
        }
    }
   
    

}
