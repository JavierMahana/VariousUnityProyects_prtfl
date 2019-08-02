using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

	public List<Conection> GetConections(Node node)
	{
		return node.conexiones;
	}

}
