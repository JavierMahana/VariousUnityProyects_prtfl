using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackComand : IComand {

    private Unit atacker_;
    private Unit defender_;
    private int beforeDefHp;

    public AtackComand(Unit atacker, Unit defender)
    {
        atacker_ = atacker;
        defender_ = defender;
    }

    public void Execute()
    {
        beforeDefHp = defender_.hP_;
        Fight.StartFight(atacker_, defender_);
    }

    public void Undo()
    {
        defender_.hP_ = beforeDefHp;
    }
}
