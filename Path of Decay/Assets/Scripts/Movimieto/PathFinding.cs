using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour {


    //deberia tener algo que lo limita a que la meta no pueda ser
    //otra que no este dentro de las casillas que tienen un "canMoveTile"

    #region Drishkas
    public static List<Casilla> Driskas(Unit selectedUnit, Casilla meta)
    {
        NodeRecord startRecord;
        NodeRecord current;

        current = new NodeRecord();

        NodeRecord endNodeRecord;

        List<Conexion> conections;

        List<NodeRecord> open;
        List<NodeRecord> closed;


        //Initialize the record for the start node
        List<Casilla> casillas = new List<Casilla>();
        startRecord = new NodeRecord();
        startRecord.node = selectedUnit.casillaActual;
        //conexion
        startRecord.costSoFar = 0;

        //initialize the open and closed list

        open = new List<NodeRecord>();
        open.Add(startRecord);
        closed = new List<NodeRecord>();


        //Iterate throught processing each node
        while (open.Count > 0)
        {
            //Sort the open list 
            open.Sort();

            //find the smallest element in the open list

            
            current = open.First<NodeRecord>();

            // if it the current nodeRecord cost so far is higher than
            // the unit remaining movement it means that theres no more nodes that you can go.
            if (current.node == meta || current.costSoFar > selectedUnit.remainingMovement)
            {
                break;
            }

            //otherwise get its outgoing conections

            conections = Map.GetConections(current.node);

            //loop through each conection

            foreach (Conexion conection in conections)
            {

                //get the costSoFar of the end nodes
                Casilla endNode = conection.finConexion;
                int endNodeCost;


                //Aca se ve que tipo de movimiento tiene la unidad y se asigna el costo en pos de esó

                if (selectedUnit.clase.tipoDeMovimiento.name == "Infanteria")
                {
                    endNodeCost = current.costSoFar + conection.cost.FootMC;
                }
                else if (selectedUnit.clase.tipoDeMovimiento.name == "Caballeria")
                {
                    endNodeCost = current.costSoFar + conection.cost.CaballaryMC;
                }
                else if (selectedUnit.clase.tipoDeMovimiento.name == "Maritimo")
                {
                    endNodeCost = current.costSoFar + conection.cost.SeaMC;
                }
                //if (unit.clase.tipoDeMovimiento.name == "Volador")
                else
                {
                    endNodeCost = current.costSoFar + conection.cost.FlyingMC;
                }


                //OJO OJO OJO 
                //Skip if the node is closed 
                if (closed.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    continue;
                }

                else if (open.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    endNodeRecord = open.Find(nodeRecord => nodeRecord.node == endNode);

                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }
                }
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }
            
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.conection = conection;

                if (open.Any(nodeRecord => nodeRecord.node == endNode) == false)
                {
                    open.Add(endNodeRecord);
                }
       
            }

            //We have finished looking at the conections
            //for the current node... so remove it from de open
            //list and add it to the closed list

            open.Remove(current);
            closed.Add(current);

        }

        if (current.node != meta)
        {
            return null;
        }
        else
        {
            while (current.node != selectedUnit.casillaActual)
            {
                casillas.Add(current.node);
                current = current.conection.GetNodeRecordInicio(closed);
            }
            casillas.Add(selectedUnit.casillaActual);
        }

        
        //se invierte las casilla, ya que fueron revisadas al reves
        casillas.Reverse();
        return casillas;
    }
    #endregion

    #region Driskas ---- devolviendo el ultimo NR
    public static List<Casilla> Driskas(Unit selectedUnit, Casilla meta, out NodeRecord finalNR)
    {
        NodeRecord startRecord;
        NodeRecord current;

        current = new NodeRecord();

        NodeRecord endNodeRecord;

        List<Conexion> conections;

        List<NodeRecord> open;
        List<NodeRecord> closed;


        //Initialize the record for the start node
        List<Casilla> casillas = new List<Casilla>();
        startRecord = new NodeRecord();
        startRecord.node = selectedUnit.casillaActual;
        //conexion
        startRecord.costSoFar = 0;

        //initialize the open and closed list

        open = new List<NodeRecord>();
        open.Add(startRecord);
        closed = new List<NodeRecord>();


        //Iterate throught processing each node
        while (open.Count > 0)
        {
            //Sort the open list 
            open.Sort();

            //find the smallest element in the open list


            current = open.First<NodeRecord>();

            // if it the current nodeRecord cost so far is higher than
            // the unit remaining movement it means that theres no more nodes that you can go.
            if (current.node == meta || current.costSoFar > selectedUnit.remainingMovement)
            {
                break;
            }

            //otherwise get its outgoing conections

            conections = Map.GetConections(current.node);

            //loop through each conection

            foreach (Conexion conection in conections)
            {

                //get the costSoFar of the end nodes
                Casilla endNode = conection.finConexion;
                int endNodeCost;


                //Aca se ve que tipo de movimiento tiene la unidad y se asigna el costo en pos de esó

                if (selectedUnit.clase.tipoDeMovimiento.name == "Infanteria")
                {
                    endNodeCost = current.costSoFar + conection.cost.FootMC;
                }
                else if (selectedUnit.clase.tipoDeMovimiento.name == "Caballeria")
                {
                    endNodeCost = current.costSoFar + conection.cost.CaballaryMC;
                }
                else if (selectedUnit.clase.tipoDeMovimiento.name == "Maritimo")
                {
                    endNodeCost = current.costSoFar + conection.cost.SeaMC;
                }
                //if (unit.clase.tipoDeMovimiento.name == "Volador")
                else
                {
                    endNodeCost = current.costSoFar + conection.cost.FlyingMC;
                }


                //OJO OJO OJO 
                //Skip if the node is closed 
                if (closed.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    continue;
                }

                else if (open.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    endNodeRecord = open.Find(nodeRecord => nodeRecord.node == endNode);

                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }
                }
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }

                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.conection = conection;

                if (open.Any(nodeRecord => nodeRecord.node == endNode) == false)
                {
                    open.Add(endNodeRecord);
                }

            }

            //We have finished looking at the conections
            //for the current node... so remove it from de open
            //list and add it to the closed list

            open.Remove(current);
            closed.Add(current);

        }

        if (current.node != meta)
        {
            finalNR = null;
            return null;
        }
        else
        {
            finalNR = current;
            while (current.node != selectedUnit.casillaActual)
            {
                casillas.Add(current.node);
                current = current.conection.GetNodeRecordInicio(closed);
            }
            casillas.Add(selectedUnit.casillaActual);
        }


        //se invierte las casilla, ya que fueron revisadas al reves
        casillas.Reverse();
        return casillas;
    }
    #endregion









    public static List<Casilla> GetListOfWalkableTiles(Unit unit)
    {


        

        NodeRecord startRecord;
        NodeRecord current;

        NodeRecord endNodeRecord;

        List<Conexion> conections;

        List<NodeRecord> open;
        List<NodeRecord> closed;


        //Initialize the record for the start node
        List<Casilla> casillas = new List<Casilla>();
        startRecord = new NodeRecord();
        startRecord.node = unit.casillaActual;
        //conexion
        startRecord.costSoFar = 0;

        //initialize the open and closed list

        open = new List<NodeRecord>();
        open.Add(startRecord);
        closed = new List<NodeRecord>();


        //Iterate throught processing each node
        while (open.Count > 0)
        {
            //Sort the open list 
            open.Sort();

            //find the smallest element in the open list

            current = new NodeRecord();
            current = open.First<NodeRecord>();

            // if it the current nodeRecord cost so far is higher than
            // the unit remaining movement it means that theres no more nodes that you can go.
            if (current.costSoFar > unit.remainingMovement)
            {
                break;
            }

            //otherwise get its outgoing conections

            conections = Map.GetConections(current.node);

            //loop through each conection

            foreach (Conexion conection in conections)
            {

                //get the costSoFar of the end nodes
                Casilla endNode = conection.finConexion;
                int endNodeCost;


                //Aca se ve que tipo de movimiento tiene la unidad y se asigna el costo en pos de esó

                if (unit.clase.tipoDeMovimiento.name == "Infanteria")
                {
                    endNodeCost = current.costSoFar + conection.cost.FootMC;
                }
                else if (unit.clase.tipoDeMovimiento.name == "Caballeria")
                {
                    endNodeCost = current.costSoFar + conection.cost.CaballaryMC;
                }
                else if (unit.clase.tipoDeMovimiento.name == "Maritimo")
                {
                    endNodeCost = current.costSoFar + conection.cost.SeaMC;
                }
                //if (unit.clase.tipoDeMovimiento.name == "Volador")
                else
                {
                    endNodeCost = current.costSoFar + conection.cost.FlyingMC;
                }


                //OJO OJO OJO 
                //Skip if the node is closed 
                if (closed.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    continue;
                }

                else if (open.Any(nodeRecord => nodeRecord.node == endNode))
                {
                    endNodeRecord = open.Find(nodeRecord => nodeRecord.node == endNode);

                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }
                }
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }
                //lo comente para probar
                /*if (ContainsNodeRecordInList(endNode, closed))
                {
                    continue;
                }
                
                //.. or if it is open and we have found a worse route
                else if (ContainsNodeRecordInList(endNode, open))
                {
                    endNodeRecord = FindNodeRecordInList(endNode, open);

                    if (endNodeRecord.costSoFar <= endNodeCost)
                    {
                        continue;
                    }

                }
                
                //if we havent Skipped it is a unvisited node
                //so we add it to the open list
                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }
                */
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.conection = conection;

                if (open.Any(nodeRecord => nodeRecord.node == endNode) == false)
                {
                    open.Add(endNodeRecord);
                }
                /* tambien lo comente para probar
                if (ContainsNodeRecordInList(endNode, open) == false)
                {
                    open.Add(endNodeRecord);
                }
                */
            }

            //We have finished looking at the conections
            //for the current node... so remove it from de open
            //list and add it to the closed list

            open.Remove(current);
            closed.Add(current);

        }
        
        foreach (NodeRecord nodeRecord in closed)
        {
            casillas.Add(nodeRecord.node);
        }

        return casillas;

    }


   
}
