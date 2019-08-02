using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuingState : UnitState
{
    public override UnitState HandleTransitions(Unit unit)
    {
        if (Vector2.Distance(unit.body2D.position, unit.Target.body2D.position) < unit.attackRange)
        {
            return fighting;
        }
        return null;
    }
    public override void Update(Unit unit)
    {
    }
    public override void OnExit(Unit unit)
    {
    }
    public override void OnEnter(Unit unit)
    {
    }
}

