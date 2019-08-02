using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding {


	//OJO CON LAS FUNCIONES CONTAINS Y FIND



	List<Conection> path;

	NodeRecord startRecord;
	NodeRecord current;

	NodeRecord endNodeRecord;

	List<Conection> conections;

	List<NodeRecord> open;
	List<NodeRecord> closed;

	public bool ContainsNodeRecordInList(Node node, List<NodeRecord> containerList)
	{
		foreach (NodeRecord nodeRecord in containerList) {
			if (nodeRecord.node == node) {
				return true;
			}
		}
		return false;

	}

	public NodeRecord FindNodeRecordInList (Node node, List<NodeRecord> containerList)
	{
		NodeRecord emptyNodeRecord;
		emptyNodeRecord = new NodeRecord ();


		foreach (NodeRecord nodeRecord in containerList) {
			if (nodeRecord.node == node) {
				return nodeRecord;
			}
		}
		return emptyNodeRecord;

	}
		
	public static NodeRecord SmallestElement(List<NodeRecord> openList)
	{
		NodeRecord smallest = new NodeRecord ();

		//ESTO DEBERIA SER INFINITO
		smallest.costSoFar = 99999;

		foreach (NodeRecord record in openList) 
		{
			if (record.costSoFar < smallest.costSoFar) {
				smallest = record;
			}
		}
		return smallest;
	}


	public List<Conection> PathFindDijkstra(Map graph, Node start, Node end)
	{

		//Initialize the record for the start node

		startRecord = new NodeRecord ();
		startRecord.node = start;
		startRecord.conection = null;
		startRecord.costSoFar = 0;

		//initialize the open and closed list

		open = new List<NodeRecord> ();
		open.Add (startRecord);
		closed = new List<NodeRecord> ();


		//Iterate throught processing each node
		while (open.Count>0) {

			//find the smallest element in the open list
			current = new NodeRecord();
			current = SmallestElement (open);

			// if it is the goal terminate
			if (current.node == end) {
				break;
			}

			//otherwise get its outgoing conections

			conections = graph.GetConections (current.node);

			//loop through each conection

			foreach (Conection conection in conections) {

				//get the costSoFar of the end nodes
				Node endNode = conection.GetToNode(conection);

				int endNodeCost = current.costSoFar + conection.GetCost (conection);


				//Skip if the node is closed 
				if (ContainsNodeRecordInList(endNode,closed)) {
					continue;
				}

				//.. or if it is open and we have found a worse route
				else if (ContainsNodeRecordInList(endNode,open)) {
					endNodeRecord = FindNodeRecordInList(endNode,open);

					if (endNodeRecord.costSoFar <=endNodeCost) {
						continue;
					} 

				} 

				//if we havent Skipped it is a unvisited node
				//so we add it to the open list
				else 
				{
					endNodeRecord = new NodeRecord ();
					endNodeRecord.node = endNode;
				}

				endNodeRecord.costSoFar = endNodeCost;
				endNodeRecord.conection = conection;

				if (ContainsNodeRecordInList(endNode,open) == false) {
					open.Add (endNodeRecord);
				}
			}

			//We have finished looking at the conections
			//for the current node... so remove it from de open
			//list and add it to the closed list

			open.Remove (current);
			closed.Add (current);

		}

		//we are here if there is no way to the goal. 
		//or if we found the goal

		if (current.node != end) {
			//theres no path to the goal
			return null;
		} 
		else {
			//we foud the goal!!!
			//now lets create the path
			while (current.node != start) {
				path.Add (current.conection);
				current = FindNodeRecordInList(current.conection.GetFromNode (current.conection),closed);
			}

		}

		//We reverse the path and return in
		path.Reverse();
		return path;


	}


	public List<Node> CreateCanMoveToTiles (Map graph, Node start, int unitMovement)
	{
		
		//Initialize the record for the start node
		List<Node> nodes = new List<Node>();
		startRecord = new NodeRecord ();
		startRecord.node = start;
		startRecord.conection = null;
		startRecord.costSoFar = 0;

		//initialize the open and closed list

		open = new List<NodeRecord> ();
		open.Add (startRecord);
		closed = new List<NodeRecord> ();


		//Iterate throught processing each node
		while (open.Count>0) {

			//find the smallest element in the open list
			current = new NodeRecord();
			current = SmallestElement (open);

			// if it the current nodeRecord cost so far is higher than
			// the unit movement it means that theres no more nodes that you can go.
			if (current.costSoFar > unitMovement ) {
				break;
			}

			//otherwise get its outgoing conections

			conections = graph.GetConections (current.node);

			//loop through each conection

			foreach (Conection conection in conections) {

				//get the costSoFar of the end nodes
				Node endNode = conection.GetToNode(conection);

				int endNodeCost = current.costSoFar + conection.GetCost (conection);


				//Skip if the node is closed 
				if (ContainsNodeRecordInList(endNode,closed)) {
					continue;
				}

				//.. or if it is open and we have found a worse route
				else if (ContainsNodeRecordInList(endNode,open)) {
					endNodeRecord = FindNodeRecordInList(endNode,open);

					if (endNodeRecord.costSoFar <=endNodeCost) {
						continue;
					} 

				} 

				//if we havent Skipped it is a unvisited node
				//so we add it to the open list
				else 
				{
					endNodeRecord = new NodeRecord ();
					endNodeRecord.node = endNode;
				}

				endNodeRecord.costSoFar = endNodeCost;
				endNodeRecord.conection = conection;

				if (ContainsNodeRecordInList(endNode,open) == false) {
					open.Add (endNodeRecord);
				}
			}

			//We have finished looking at the conections
			//for the current node... so remove it from de open
			//list and add it to the closed list

			open.Remove (current);
			closed.Add (current);

		}


		foreach (NodeRecord nodeRecord in closed) {
			nodes.Add (nodeRecord.node);
			}

		return nodes;

	}


}
