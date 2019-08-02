using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding  {

    public class NodeRecord
    {
        public Tile_ node;
        public int costSoFar;
        public Conection conection;
        public int estimatedCost;
    }

    public static List<Tile_> ShortestPath(Map map, Tile_ startNode, Tile_ endNode)
    {
        List<Tile_> open = new List<Tile_>();
        List<Tile_> closed = new List<Tile_>();
        Tile_ current = null;
        open.Add(startNode);
        Debug.Log("se agregan a la open " + startNode.position.ToString());
        while (open.Count > 0)
        {
            current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost || current.fCost == open[i].fCost && open[i].hCost < current.hCost)
                {
                    open[i] = current;
                }
            }
            Debug.Log("current: " + current.position.ToString());
            open.Remove(current);
            closed.Add(current);
            if (current == endNode)
            {
                Debug.Log("wiiiii");
                break;
            }

            foreach (Tile_ neightbour in GetNeigthbours(map, current))
            {
                //aca puedo agragar condiciones
                if (neightbour.walkable == false || closed.Contains(neightbour))
                {
                    continue;
                }
                int newGCost = current.hCost + 1;
                if (open.Contains(neightbour) == false || newGCost < neightbour.gCost)
                {
                    Debug.Log("se agregan a la open " + neightbour.position.ToString());
                    neightbour.gCost = newGCost;
                    neightbour.hCost = GetDistance(neightbour, endNode);
                    neightbour.parent = current;
                    open.Add(neightbour);
                }
            }
        }
        if (current != endNode)
        {
            return null;
        }
        return RetracePath(startNode, endNode);
    }

    static List<Tile_> RetracePath(Tile_ startNode, Tile_ endNode)
    {
        List<Tile_> path = new List<Tile_>();

        Tile_ current = endNode;
        while (current != startNode)
        {
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }
    static List<Tile_> GetNeigthbours(Map map, Tile_ node)
    {
        int x = node.position.x;
        int y = node.position.y;
        int xMax = map.xMax;
        int yMax = map.yMax;
        int xMin = map.xMin;
        int yMin = map.yMin;

        List<Tile_> neigthbours = new List<Tile_>();
        if (x > xMin)
        {
            neigthbours.Add( map.GetTile(x - 1, y));
        }
        if (x < xMax - 1)
        {
            neigthbours.Add(  map.GetTile(x + 1, y));
        }
        if (y > yMin)
        {
            neigthbours.Add( map.GetTile(x, y - 1));
        }
        if (y < yMax - 1)
        {
            neigthbours.Add( map.GetTile(x, y + 1));
        }
        return neigthbours;
    }
    public static List<Tile_> AStar(Map map, Tile_ start, Tile_ goal)
    {
        List<Tile_> output = new List<Tile_>();

        List<NodeRecord> open = new List<NodeRecord>();
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord startRecord = new NodeRecord();

        startRecord.node = start;
        startRecord.estimatedCost = GetDistance(start, goal);
        startRecord.costSoFar = 0;
        startRecord.conection = null;

        open.Add(startRecord);

        NodeRecord current = startRecord;

        int yMax, yMin, xMax, xMin;

        yMin = (int)map.origin.y;
        yMax = (int)map.origin.y + map.gridSizeY;
        xMin = (int)map.origin.x;
        xMax = (int)map.origin.x + map.gridSizeX;

        //ver si se devuelve la parte de la lista cerrada
        while (open.Count > 0)
        {
            current = EstimatedSmallestNodeInList(open);
            if (current.node == goal )
            {
                break;
            }
            
            int x = current.node.position.x;
            int y = current.node.position.y;


            if (x > xMin)
            {
                Tile_ endTile = map.GetTile(x - 1, y);
                UpdateOrCreateRecordInOpenList(goal, open, closed, current, endTile);
            }
            if (x < xMax - 1)
            {
                Tile_ endTile = map.GetTile(x + 1, y);
                UpdateOrCreateRecordInOpenList(goal, open, closed, current, endTile);
            }
            if (y > yMin)
            {
                Tile_ endTile = map.GetTile(x, y - 1);
                UpdateOrCreateRecordInOpenList(goal, open, closed, current, endTile);
            }
            if (y < yMax - 1)
            {
                Tile_ endTile = map.GetTile(x, y + 1);
                UpdateOrCreateRecordInOpenList(goal, open, closed, current, endTile);
            }
            closed.Add(current);
            open.Remove(current);
            
        }
        //Se acabaron las open Tiles o llegamos a la meta;
        if (current.node != goal)
        {
            return null;
        }
        
        closed.Add(current);

        Tile_ retraceNode = current.node;
        while (retraceNode != start)
        {
            Debug.Log(current.node.position.ToString());
            Conection conection = current.conection;
            output.Add(conection.toNode);
            retraceNode = conection.fromNode;

            current = Find(closed, retraceNode);

        }

        //output.Add(start);

        output.Reverse();
        
        return output;

    }

    private static void UpdateOrCreateRecordInOpenList(Tile_ goal, List<NodeRecord> open, List<NodeRecord> closed, NodeRecord current, Tile_ endTile)
    {
        if (endTile.walkable != false)
        {
            return;
        }
        if (CheckTileInOpenList(goal, open, current, endTile)) { }
        else if (Contains(closed, endTile)) { }//CheckTileInClosedList(goal, open, closed, current, endTile)
        else
        {
            NodeRecord newRecord = new NodeRecord();
            newRecord.node = endTile;
            newRecord.costSoFar = current.costSoFar + 1;
            newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
            newRecord.conection = new Conection(current.node, endTile);
            open.Add(newRecord);
        }
    }

    private static bool CheckTileInClosedList(Tile_ goal, List<NodeRecord> open, List<NodeRecord> closed, NodeRecord current, Tile_ endTile)
    {
        if (Contains(closed, endTile))
        {
            int newCostSoFar = 1 + current.costSoFar;
            NodeRecord oldRecord = Find(closed, endTile);
            if (oldRecord.costSoFar > newCostSoFar)
            {
                NodeRecord newRecord = oldRecord;
                newRecord.costSoFar = newCostSoFar;
                newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                newRecord.conection = new Conection(current.node, endTile);

                closed.Remove(oldRecord);
                open.Add(newRecord);
            }
            return true;
        }
        return false;
    }

    private static bool CheckTileInOpenList(Tile_ goal, List<NodeRecord> open, NodeRecord current, Tile_ endTile)
    {
        if (Contains(open, endTile))
        {
            int newCostSoFar = 1 + current.costSoFar;
            NodeRecord oldRecord = Find(open, endTile);
            if (oldRecord.costSoFar > newCostSoFar)
            {
                NodeRecord newRecord = oldRecord;
                newRecord.node = endTile;
                newRecord.costSoFar = newCostSoFar;
                newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                newRecord.conection = new Conection(current.node, endTile);

            }
            return true;
        }
        return false;
    }






    static NodeRecord EstimatedSmallestNodeInList(List<NodeRecord> list)
    {
        NodeRecord smallestNode = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].estimatedCost < smallestNode.estimatedCost)
            {
                smallestNode = list[i];
            }
        }
        
        return smallestNode;

    }
    static bool Contains(List<NodeRecord> list, Tile_ node)
    {
        foreach (NodeRecord record in list)
        {
            if (record.node == node)
            {
                return true;
            }
        }
        return false;
    }
    static NodeRecord Find(List<NodeRecord> list, Tile_ node)
    {
        foreach (NodeRecord record in list)
        {
            if (record.node == node)
            {
                return record;
            }
        }
        
        return null;
    }
    static NodeRecord SmallestNodeInListCSF(List<NodeRecord> list)
    {
        NodeRecord smallestNode = list[0];
        foreach (NodeRecord record in list)
        {
            if (record.costSoFar < smallestNode.costSoFar)
            {
                smallestNode = record;
            }
        }
        return smallestNode;
    }
    static int GetDistance(Tile_ t1, Tile_ t2)
    {
        int xDif = Mathf.Abs((int)(t1.position.x - t2.position.x));
        int yDif = Mathf.Abs((int)(t1.position.y - t2.position.y));
        int distance = xDif + yDif;
        return distance;
    }
}
