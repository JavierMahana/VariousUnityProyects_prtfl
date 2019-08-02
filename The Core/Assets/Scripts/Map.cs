using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour {


    public Tilemap sourceMap;
    public List<Tilemap> notWalkableTilemaps;

    public int yMax,yMin,xMax,xMin;
    public Vector2 origin;
    public int gridSizeX, gridSizeY;

    Vector2 worldSize;
    
    float nodeDiameter;

    float nodeRadius;
    
    
    Tile_[,] grid;
    void Awake()
    {
        origin = new Vector2(sourceMap.origin.x, sourceMap.origin.y);
        worldSize = new Vector2(sourceMap.size.x, sourceMap.size.y);
        nodeDiameter = 1;
        nodeRadius = nodeDiameter / 2;
        gridSizeX = Mathf.FloorToInt(worldSize.x / nodeDiameter);
        gridSizeY = Mathf.FloorToInt(worldSize.y / nodeDiameter);
        yMin = (int)origin.y;
        yMax = (int)origin.y + gridSizeY;
        xMin = (int)origin.x;
        xMax = (int)origin.x + gridSizeX;

        generateGrid();
    }

    public Tile_ GetTile(int x, int y)
    {
        return grid[x, y];
    }

    public void generateGrid()
    {
        grid = new Tile_[gridSizeX,gridSizeY];

        int yMin = (int)origin.y;
        int yMax = (int)origin.y + gridSizeY;
        int xMin = (int)origin.x;
        int xMax = (int)origin.x + gridSizeX;

        for (int y = yMin; y < yMax; y++)
        {
            for (int x = xMin; x < xMax; x++)
            {
                
                Vector3Int currentCoordinates = new Vector3Int(x, y, 0);
                if (sourceMap.GetTile(currentCoordinates) != null)
                {
                    
                    bool walkableTile = true;
                    if (notWalkableTilemaps != null)
                    {
                        foreach (Tilemap nwMap in notWalkableTilemaps)
                        {
                            if (nwMap.GetTile(currentCoordinates) != null)
                            {
                                walkableTile = false;
                                break;
                            }
                        }
                    }
                    Tile_ newTile = new Tile_(new Vector2Int(x,y), false, walkableTile);
                    grid[x, y] = newTile;
                }
            }
        }
        
    }


}
