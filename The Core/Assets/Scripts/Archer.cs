using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Archer : Unit , IDamagable, IAtacker {

    public float atackRange;
    public float visionRange;
    


    void Update()
    {
        CheckForState();
    }
    public void ExecuteQueue()
    {

    }

    public void CheckForState()
    {
        switch (state)
        {
            case UnitState.Fighting:
                OnFigth();
                break;
            case UnitState.Persuing:
                OnPersue();
                break;
            case UnitState.Idle:
                Idle();
                break;
        }
    }
    public void OnFigth()
    {

    }
    public void OnPersue()
    {
    }
    public void Idle()
    {

    }
    public void Atack()
    {

    }
    public void TakeDamage()
    {
    }
	
}
