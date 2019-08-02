using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public int x;
	public int y;

	public List<Conection> conexiones;


	public Node()
	{
		conexiones = new List<Conection> ();
	}

}
