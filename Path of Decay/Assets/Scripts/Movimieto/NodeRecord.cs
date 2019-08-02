using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeRecord : IComparable<NodeRecord> {

    public Casilla node;
    public Conexion conection;
    public int costSoFar;

    public int CompareTo(NodeRecord other)
    {
        return this.costSoFar.CompareTo(other.costSoFar);
    }


}
