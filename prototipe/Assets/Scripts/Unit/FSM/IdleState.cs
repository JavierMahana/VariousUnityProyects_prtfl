using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : UnitState
{
    const float distToGetBackAtStayingPosition = 1;

    public override UnitState HandleTransitions(Unit unit)
    {
        if (unit.destination != null)
        {
            //doble click? o single click
        }
        return null;
    }
    public override void Update(Unit unit)
    {
        if (unit.idleCounter > 3)
        {
            if (Vector2.Distance(unit.body2D.position, unit.stayingPos) > distToGetBackAtStayingPosition)
            {
                unit.Move(unit.stayingPos);
            }
            //se podria poner acá una nueva staying position, en la posicion actual. Me gustaría probar ambas formas.
        }
        unit.idleCounter += Time.deltaTime ;
    }
    public override void OnExit(Unit unit)
    {
    }
    public override void OnEnter(Unit unit)
    {
        unit.idleCounter = 0;
    }
}

