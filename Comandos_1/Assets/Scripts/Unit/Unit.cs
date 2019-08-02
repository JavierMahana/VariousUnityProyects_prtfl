using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    Unit(string name, int hP, int atack)
    {
        unitName_ = name;
        hP_ = hP;
        atack_ = atack;
    }

    public string unitName_;
    public int hP_;
    public int atack_;
}
