using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionHandler  {
    //Read Only
    public static Unit selectedUnit { get; private set; }

    public static void SelectUnit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, 1 << 8))
        {
            selectedUnit = hit.transform.GetComponent<Unit>();
        }
        else
            return;
    }
	
}
