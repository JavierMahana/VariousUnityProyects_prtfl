using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Fight {

    public static void StartFight(Unit atacker, Unit defender)
    {
        Debug.Log(atacker.unitName_ + " Atack " + defender.unitName_);
        defender.hP_ -= atacker.atack_;
        if (defender.hP_< 0)
        {
            defender.hP_ = 0;
        }
        if (defender.hP_ == 0)
        {
            Debug.Log(defender.unitName_ + " dies in the fight!");
        }

    }
	
}
