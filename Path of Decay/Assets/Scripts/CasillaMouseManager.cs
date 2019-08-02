using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaMouseManager {

    public static void ActualizarCasillaMouse(out Casilla casillaMouse)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 10, 1 << 8))
        {
            casillaMouse = hitInfo.transform.GetComponent<Casilla>();
        }
        else
            casillaMouse = null;


    }
}
