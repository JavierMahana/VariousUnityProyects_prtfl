using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMoveState : UnitState {
    //podria almacenar la path acá.
    public override UnitState HandleTransitions(Unit unit)
    {

        if (unit.Arrived())
        {
            return idle;
        }

        Unit enemy = unit.CheckForEnemies();
        if (enemy != null)
        {
            unit.Target = enemy;
            return persuing;
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
