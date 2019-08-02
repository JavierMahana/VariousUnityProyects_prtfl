using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public static List<Conexion> GetConections(Casilla nodo)
    {
        return nodo.conexiones;
    }

}
