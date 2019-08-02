using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : UnitState {

    public override UnitState HandleTransitions(Unit unit)
    {

        if (unit.Arrived())
        {
            return idle;
        }
        return null;
    }
    public override void Update(Unit unit)
    {
        unit.Move((Vector2)unit.destination);
    }
    public override void OnExit(Unit unit)
    {
       
    }
    public override void OnEnter(Unit unit)
    {

    }
    
}
