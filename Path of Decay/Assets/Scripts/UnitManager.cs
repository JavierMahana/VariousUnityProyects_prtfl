using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    

    //Seria bueno que tenga una funcion para asignar todas las casillas en las cuales se encuentran las unidades
    public static void ActualizarTodasLasUnidades(GameManager gm)
    {
        

        foreach (Unit unit in gm.allUnits)
        {
         
            GameObject unitGO = unit.gameObject;
            //Sprite
            SpriteRenderer unitSR = unitGO.GetComponentInChildren<SpriteRenderer>();
            unitSR.sprite = unit.clase.defaultSrite;
            //Casilla Actual
            int x = Mathf.FloorToInt(unitGO.transform.position.x / 0.16f);
            int y = Mathf.FloorToInt(unitGO.transform.position.y / 0.16f);
            Vector2Int coordenadasUnit = new Vector2Int(x,y);
            foreach (Casilla casilla in gm.mapa)
            {
                if (casilla.position == coordenadasUnit)
                {
                    unit.casillaActual = casilla;
                    break;
                }
            }
        }
    }

    public static void SetFriendlyUnits(GameManager gm)
    {
        foreach (Unit u in gm.allUnits)
        {
            if (u.isFriendly)
            {
                gm.friendlyUnits.Add(u);
            }
        }
    }
   
    public static void ActivarTodasLasUnidadesAmistosas(GameManager gm)
    {
        foreach (Unit unit in gm.friendlyUnits)
        {
            unit.isActive = true;
        }
    }

}
