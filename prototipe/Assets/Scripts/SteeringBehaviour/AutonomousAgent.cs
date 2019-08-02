using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AutonomousAgent : MonoBehaviour
{

    public float maxSpeed;
    public float maxForce;

    //esta wea esta rota(mal hecha cambiar el sistema)
    public MovementBehaviour[] movementBehaviours;
    

    Rigidbody2D body2D;

    Vector2 steeringForce = Vector2.zero;

    Vector2[] visualRepOfSteer;

    void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
        visualRepOfSteer = new Vector2[movementBehaviours.Length];
    }

    void FixedUpdate()
    {
        CalculateForces();
    }


    void CalculateForces()
    {
        steeringForce = Vector2.zero;
        for (int i = 0; i < movementBehaviours.Length; i++)
        {
            MovementBehaviour currentBehaviour = movementBehaviours[i];

            Vector2 currentForceToAdd = currentBehaviour.behaviour.GetSteering(body2D, maxForce, maxSpeed) * currentBehaviour.weigth;

            visualRepOfSteer[i] = currentForceToAdd; //for gizmos

            steeringForce += currentForceToAdd;
            //if (AcumulateForces(currentForceToAdd) == false)
            //{
            //    break;
            //}
        }
        //float steeringMagnitude = steeringForce.magnitude;
        //if (steeringMagnitude > maxSpeed)
        //{
        //    steeringForce = (steeringForce / steeringMagnitude) * maxSpeed;
        //}
        body2D.AddForce(steeringForce, ForceMode2D.Impulse);
        body2D.velocity = body2D.velocity.ShortenMagnitude(maxSpeed);

    }

    bool AcumulateForces(Vector2 forceToAdd)
    {
        //--------------------------------------------------------------------- ACA HAY UNA COSA QUE NO ME GUSTA MUCHO.
        //--------------------------------------------------------------------- EN SUMAS DE VALORES AVECES SE PIERDE "FUERZA"
        float magnitudeSoFar = steeringForce.magnitude;

        float magnitudeRemaining = maxSpeed - magnitudeSoFar;
        if (magnitudeRemaining <= 0)
        {
            return false;
        }

        float magnitudeToAdd = forceToAdd.magnitude;
        if (magnitudeToAdd > magnitudeRemaining)
        {
            forceToAdd = forceToAdd / magnitudeToAdd * magnitudeRemaining;
        }
        steeringForce += forceToAdd;
        return true;



    }

    void OnDrawGizmos()
    {
        if (body2D)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(body2D.position, body2D.position + body2D.velocity);

            if (steeringForce != Vector2.zero)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(body2D.position, body2D.position + steeringForce);
            }

            if (visualRepOfSteer.Length != 0)
            {
                Gizmos.color = Color.black;
                for (int i = 0; i < visualRepOfSteer.Length; i++)
                {
                    Gizmos.DrawLine(body2D.position, body2D.position + visualRepOfSteer[i]);
                }
            }
        }
        
    }
}
        



[System.Serializable]
public struct MovementBehaviour
{
    public Steering behaviour;
    [Range(0,1)]
    public float weigth;
}
