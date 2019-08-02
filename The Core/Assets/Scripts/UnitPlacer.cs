using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : MonoBehaviour {
    //for the units being buyed
    Map map;

    private void Start()
    {
        map = FindObjectOfType<Map>();
    }


    void Update()
    {
        Snatch();
        if (Input.GetMouseButtonDown(0))
        {
            Tile_ tile = map.GetTile((int)transform.position.x, (int)transform.position.y);
            if (Place(tile))
            {
                GetComponent<UnitPlacer>().enabled = false;

            }
        }
    }

    public bool Place(Tile_ tile)
    {
        if (tile == null)
        {
            return false;
        }
        else if (tile.ocupied == false)
        {
            return true;
        }
        return false;
    }

    void Snatch()
    {
        Vector3 posToSnatch;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(mousePos.x);
        int y = Mathf.FloorToInt(mousePos.y);

        posToSnatch = new Vector3(x, y, 0);

        transform.position = posToSnatch;
    }
}
