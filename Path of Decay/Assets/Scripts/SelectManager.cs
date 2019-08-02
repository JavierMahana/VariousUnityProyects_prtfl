using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager {

    public static void ControlarSeleccion(out Unit unit)
    {
        unit = Selecionar();
    }

    static Unit Selecionar()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9))
        {
            return hit.transform.GetComponent<Unit>();
        }
        return null;
    }

 

}
