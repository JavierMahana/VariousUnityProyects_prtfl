using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutonomousAgent))]
public class Unit : MonoBehaviour {

    public Unit Target
    {
        get { return target ;}
        set { target = value ;}
    }

    public float visionRange;
    public float attackRange;
    public Rigidbody2D body2D;
    public Vector2? destination;
    public Vector2 stayingPos;
    //no tiene sentido que este acá pero pico... por mientras. no quiero hacer el UFSM más caro solo por esto.
    public float idleCounter;

    private Unit target;
    private AutonomousAgent steering;
    private UnitState state;

    void Awake()
    {
        steering = GetComponent<AutonomousAgent>();
        body2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UnitState transitionState = state.HandleTransitions(this);
        if (transitionState != null)
        {
            state.OnExit(this);
            transitionState.OnEnter(this);

            state = transitionState;
        }
        
        state.Update(this);
    }

    public bool GetPath()
    {
        //steering 
        return true;
    }

    public void Move(Vector2 destinaton)
    {

    }
    public Unit CheckForEnemies()
    {
        if (true)
        {
            return null;
        }
    }

    public void Attack()
    {
    }
    public void StopAttacking()
    {   
    }

    public void DeactivatePathFollow()
    {
    }
    public bool Arrived()
    {
        return false;
    }
}
