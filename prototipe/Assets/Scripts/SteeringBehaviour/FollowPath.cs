﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AutonomousAgent))]
public class FollowPath :  Steering {
    public float pathRadious = 0.3f;
    public float lookAheadMult = 1;
    public float boomMagnitude = 1;
    public Transform destinationPosition;
    
    Path currentPath;
    PathRequestManager pathRequester;
    void Awake()
    {
        pathRequester = FindObjectOfType<PathRequestManager>();
    }
    void Start()
    {
        SetPath(transform.position, destinationPosition.position);
    }

    public void SetPath(Vector2 startPos, Vector2 endPos)
    {
        pathRequester.RequestPath(startPos, endPos, OnPathRecived);
    }

    public override Vector2 GetSteering(Rigidbody2D body2D, float maxSteeringMagnitude, float maxSpeed)
    {
        if (currentPath != null)
        {
            return SteeringBehabiour.FollowPath(body2D, currentPath, maxSteeringMagnitude, maxSpeed, lookAheadMult, boomMagnitude);       
        }
        return Vector2.zero;
    }

    public void OnPathRecived(Vector2[] _path, bool succes)
    {
        if (succes)
        {
            currentPath = new Path(_path, pathRadious);
        }
    }
}
