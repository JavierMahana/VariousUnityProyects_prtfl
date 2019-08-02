using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
	
	//variables para el moviemiento de las unidades

	public GameObject selectedUnit;



	//variables para creacion del graph




	public int mapSizeX = 10;
	public int mapSizeY = 10;

	public GameObject mapGO;
	Map map;


	public Node[,] graph;

	//variables para generar el mapa y tener el costo de las conecciones}

	int[,] tiles;

	//0 = grassland;
	//1 = swamp;
	//2 = mountian, 
	public TileType[] tileTypes;







	void Start () 
	{
		mapGO = GameObject.FindObjectOfType<Map> ().gameObject;
		map = mapGO.GetComponent<Map>();
		CreateTileMap ();
		CreateGraph ();


	}

	void CreateTileMap ()
	{
		CreateMapData ();
		CreateMapPrefabs ();
	}

	void CreateMapData()
	{
		tiles = new int[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				//tiles [x, y] = new int ();
				if (x==6 && y>4) {
					tiles [x, y] = 1;
				}
				else if (y==4 && x>0) {
					tiles[x,y] = 2;
				} 
				else {
					tiles [x, y] = 0;
				}
			}
		}
	}

	void CreateMapPrefabs()
	{
		
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				
				Instantiate (tileTypes [tiles [x, y]].visualPrefab, new Vector3 (x, y, 0), Quaternion.identity, mapGO.transform);

			}
		}
	}

	void CreateGraph()
	{
		graph = new Node[mapSizeX, mapSizeY];
		CreateNodes ();
		CreateConections ();
	}



	void CreateNodes()
	{
		graph = new Node[mapSizeX, mapSizeY];

		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {

				graph [x, y] = new Node ();
				graph [x, y].x = x;
				graph [x, y].y = y;

			}
		}

	}



	int GetEnterCost(int x, int y)
	{
		return tileTypes [tiles [x, y]].costToMove;
	}


	void CreateConections()
	{



		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {

				if (x > 0) {

					graph [x, y].conexiones.Add (new Conection (
						graph [x, y], 
						graph [x - 1, y],
						map.GetEnterCost (graph [x - 1, y].x, graph [x - 1, y].y))
					);
				}
				if (x < mapSizeX - 1) {


					graph [x, y].conexiones.Add (new Conection (
						graph [x, y], 
						graph [x + 1, y],
						map.GetEnterCost (graph [x + 1, y].x, graph [x + 1, y].y))
					);
				}
				if (y > 0) {
					graph [x, y].conexiones.Add (new Conection (
						graph [x, y], 
						graph [x, y - 1],
						map.GetEnterCost (graph [x, y - 1].x, graph [x, y - 1].y))
					);
				}
				if (y < mapSizeY - 1) {
					graph [x, y].conexiones.Add (new Conection (
						graph [x, y], 
						graph [x, y + 1],
						map.GetEnterCost (graph [x, y + 1].x, graph [x, y + 1].y))
					);
				}
			}
		}
	}


	public Node GetNode(int x, int y)
	{
		return graph [x, y];
	}

	public List<Conection> GetConections(Node node)
	{
		return node.conexiones;
	}



}
