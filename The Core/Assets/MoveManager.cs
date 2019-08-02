using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour {

    SelectManager selMan;
    GameObject objectSelected;
    Map map;
    bool movableDestination;
    Vector2Int mouseCoords;
    public GameObject higlLightObject;

	void Start ()
    {
        map = FindObjectOfType<Map>();
        selMan = FindObjectOfType<SelectManager>();
	}
    void Update()
    {
        objectSelected = selMan.selectedObject;
        if (objectSelected != null)
        {
            if (objectSelected.GetComponent<IMovable>() != null)
            {
                if (Input.GetMouseButton(1))
                {
                    HighLightPosibleDestination();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    HideHighlight();
                    if (movableDestination)
                    {
                        //objectSelected.GetComponent<Unit>().Test(mouseCoords);
                        //StartCoroutine(objectSelected.GetComponent<IMovable>().Move(mouseCoords));
                    }
                    
                }
                
            }
        }
    }

    void HighLightPosibleDestination()
    {
        //azul si es posible
        //rojo si no lo es
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(mouseWorldPos.x);
        int y = Mathf.FloorToInt(mouseWorldPos.y);

        mouseCoords = new Vector2Int(x, y);
        Tile_ tile = map.GetTile(x, y);

        Vector3 hPos = new Vector3(x, y, 0);

        higlLightObject.SetActive(true);
        higlLightObject.transform.position = hPos;

        SpriteRenderer render = higlLightObject.GetComponent<SpriteRenderer>();
        if (tile.ocupied || tile.walkable == false)
        {
            render.color = new Color(1, 0, 0, 0.5f);
            movableDestination = false;
        }
        else
        {
            render.color = new Color(0, 1, 1, 0.5f);
            movableDestination = true;
        }
            

    }
    void HideHighlight()
    {
        higlLightObject.SetActive(false);
    }
}
