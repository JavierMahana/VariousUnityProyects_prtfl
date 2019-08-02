using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : UnitState {

    public override UnitState HandleTransitions(Unit unit)
    {
        
        if (Vector2.Distance(unit.body2D.position , unit.Target.body2D.position) > unit.attackRange)
        {
            return persuing;
        }
        return null;
    }
    public override void Update(Unit unit)
    {
        unit.Attack();
    }
    public override void OnExit(Unit unit)
    {
        unit.StopAttacking();
    }
    public override void OnEnter(Unit unit)
    {
        unit.DeactivatePathFollow();
    }
}
