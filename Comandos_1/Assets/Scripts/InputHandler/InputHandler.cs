using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHandler {




    public static void HandleImput()
    {
        //SELECT MANAGGER
        if (Input.GetMouseButtonDown(0))
        {
            SelectionHandler.SelectUnit();
        }



        //MOVE COMAND
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Unit selected = SelectionHandler.selectedUnit;
            MoveUnitComand mc = new MoveUnitComand(selected, 
                (int)selected.transform.position.x, ((int)selected.transform.position.y + 1));
            //Execute
            mc.Execute();
            
            //make the command the current and add it to the list
            ComandManager.recordedComands.Add(mc);
            ComandManager.current = mc;
        }




        //prueba
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Unit selected = SelectionHandler.selectedUnit;
            MoveUnitComand mc = new MoveUnitComand(selected,
                (int)selected.transform.position.x, ((int)selected.transform.position.y + 1));
            //Execute
            mc.Execute();

            //make the command the current and add it to the list
            ComandManager.recordedComands.Add(mc);
            ComandManager.current = mc;
            ComandManager.previousTurnPointer = mc;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ComandManager.ReturnToLastTurn();
            
        }


    }
    
}
