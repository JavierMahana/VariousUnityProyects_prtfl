using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conection  {

    public Tile_ toNode;
    public Tile_ fromNode;

    public Conection(Tile_ fromNode, Tile_ toNode)
    {
        this.toNode = toNode;
        this.fromNode = fromNode;
    }
}
