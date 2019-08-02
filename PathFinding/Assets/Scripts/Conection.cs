using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conection {


	Node fromNode;
	Node toNode;

	int weight;


	public int GetCost(Conection conection)
	{
		return conection.weight;
	}

	public Node GetFromNode(Conection conection)
	{
		return conection.fromNode;
	}

	public Node GetToNode(Conection conection)
	{
		return conection.toNode;
	}


	public Conection(Node _fromNode, Node _toNode, int _weight)
	{
		fromNode = _fromNode;
		toNode = _toNode;
		weight = _weight;
	}



}
