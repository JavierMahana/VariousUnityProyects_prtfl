using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitComand : IComand {

    public MoveUnitComand(Unit unit, int x, int y)
    {
        unit_ = unit;
        x_ = x;
        y_ = y;
    }

    public void Execute()
    {
       
        beforeX_ = x_;
        beforeY_ = y_;
        unit_.transform.position = (new Vector3(x_, y_, 0));
    }
    public void Undo()
    {
        unit_.transform.position = (new Vector3(beforeX_, beforeY_, 0));
    }

    private Unit unit_;
    private int x_, y_;
    private int beforeX_, beforeY_;
}
