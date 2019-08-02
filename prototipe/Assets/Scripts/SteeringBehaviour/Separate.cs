using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutonomousAgent))]
public class Separate : Steering
{
    public float separationRadious;
    public float maxRepulsion;
    public LayerMask repulsionLayer;
    Collider2D[] entitiesArray = new Collider2D[10];
    void OnValidate()
    {
        maxRepulsion = Mathf.Clamp(maxRepulsion, 0, float.PositiveInfinity);
    }

    public override Vector2 GetSteering(Rigidbody2D body2D, float maxSteeringMagnitude, float maxSpeed)
    {
        Vector2 steering = SteeringBehabiour.Separation(body2D, separationRadious, maxSteeringMagnitude, maxSpeed, repulsionLayer, ref entitiesArray);
        return steering;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(this.transform.position, separationRadious);
    }
}
