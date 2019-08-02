using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tile_ {

    public Vector2Int position;
    public bool ocupied;
    public bool walkable;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get { return gCost + hCost; }
    }
    public Tile_ parent;

    public Tile_() { }
    public Tile_(Vector2Int position, bool ocupied, bool walkable)
    {
        this.position = position;
        this.ocupied = ocupied;
        this.walkable = walkable;
    }
}
