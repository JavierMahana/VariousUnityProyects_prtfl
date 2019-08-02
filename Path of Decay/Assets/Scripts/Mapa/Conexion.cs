using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conexion {

    public Casilla inicioConexion;
    public Casilla finConexion;
    public MovementCost cost;

    public NodeRecord GetNodeRecordInicio(List<NodeRecord> closed)
    {
        return closed.Find(nodeRecord => nodeRecord.node == inicioConexion);
    }

    public Conexion(Casilla inicio, Casilla final)
    {
        inicioConexion = inicio;
        finConexion = final;
        cost = final.terreno.mc;
       
    }

   
}
