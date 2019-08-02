using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SteeringBehabiour
{
    // every vector2.distance call use .magnitude/ maybe is better to do it manualy with the sqrMagnitude.
    //se podria ver que elementos se repiten en muchas funciones.
    //como la magnitud de la velocidad y se podrian pasar como argumentos(así se calcula 1 vez enves de n veces)
    const float defaultArribeRadious = 0.5f;
    
    public static Vector2 Separation(Rigidbody2D _rigidbody, float repulsionRadious, float _maxForce, float _maxSpeed/*desuso*/, LayerMask layerMask, ref Collider2D[] entitiesAround)
    {
        Vector2 steering = Vector2.zero;
        int numOfEntities =  Physics2D.OverlapCircleNonAlloc(_rigidbody.position, repulsionRadious, entitiesAround, layerMask);

        for (int i = 0; i < numOfEntities; i++)
        {
            Vector2 ePos = entitiesAround[i].GetComponent<Rigidbody2D>().position;

            Vector2 opositeDirecction = -(ePos - _rigidbody.position).normalized;
            steering += opositeDirecction * _maxForce;
            //float dist = Vector2.Distance(ePos, _rigidbody.position);
            //if (dist == 0)
            //{
            //    continue;
            //}
            //float intensity = _maxForce * ((repulsionRadious - dist) / repulsionRadious); //maxforce * un factor de 0-1 proporcional a la distancia
            //steering += (_rigidbody.position - ePos).normalized * intensity;
        }
        return steering;
        //return steering.ShortenMagnitude(_maxForce);
    }
    public static Vector2 AvoidWalls(Rigidbody2D _rigidbody, List<Whisker> _whiskers, float _maxForce, float _maxSpeed, LayerMask wallLayer)
    {
        Vector2 steering = Vector2.zero;

        float velAngle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x);
        float velMag = _rigidbody.velocity.magnitude;
        if (_maxSpeed == 0)
        {
            Debug.LogError("maxSpeed can't be 0");
        }
        float velRelated = velMag / _maxSpeed; //0-1
        for (int i = 0; i < _whiskers.Count; i++)
        {
            Vector2 wForce;

            Whisker whisker = _whiskers[i];
            float rayDistance = velRelated * whisker.range;
            float whiskerAngle = whisker.angle + velAngle;
            Vector2 whiskerDirection = new Vector2(Mathf.Cos(whiskerAngle), Mathf.Sin(whiskerAngle));
            RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position + whisker.origin, whiskerDirection, rayDistance , wallLayer);

            if (hit)
            {
                //this is the steering of the whisker(se asalta el paso de tener un a desirted velocity)
                wForce = ((hit.normal + whiskerDirection).normalized) * (rayDistance - hit.distance);
                steering += wForce;
            }
        }
        return steering.ShortenMagnitude(_maxForce);
    }
    public static Vector2 FollowPath(Rigidbody2D _rigidbody, Path path, float _maxForce, float _maxSpeed, float _lookAhead, float _boomMagnitude)
    {
        Vector2 predictedPos = (_rigidbody.position + _rigidbody.velocity) * _lookAhead;
        Vector2 closestPointInThePath;

        if (path.index < path.waypoints.Length - 1)
        {
            if (Vector2.Distance(path.waypoints[path.index + 1], _rigidbody.position) < path.radious)
            {
                path.index++;
            }
        }
        if (path.index == path.waypoints.Length - 1)
        {
            return Arribe(_rigidbody, path.waypoints[path.index], _maxSpeed, _maxForce, path.radious / 2);
        }

        Vector2 currentSegmentOfThePathDirection;
        /* temporal */ closestPointInThePath = GetTheNormalPointOfSegmentFromPoint(predictedPos, path.waypoints[path.index], path.waypoints[path.index + 1], out currentSegmentOfThePathDirection);

        if (_rigidbody.velocity.magnitude < 0.1)
            closestPointInThePath = path.waypoints[path.index + 1];
        //else
        //    closestPointInThePath = GetTheNormalPointOfSegmentFromPoint(predictedPos, path.waypoints[path.index], path.waypoints[path.index + 1], out currentSegmentOfThePathDirection);

        if (Vector2.Distance(closestPointInThePath, predictedPos) > path.radious || _rigidbody.velocity.magnitude < _maxSpeed - 0.01)
        {
            closestPointInThePath += (currentSegmentOfThePathDirection * _boomMagnitude);
            return Seek(_rigidbody, closestPointInThePath, _maxSpeed, _maxForce);
        }

        return Vector2.zero;
    }
    public static Vector2 Seek(Rigidbody2D rigidbody, Vector2 objective, float maxSpeed, float maxForce)
    {

        Vector2 desiredVelocity = (objective - rigidbody.position).normalized * maxSpeed;
        Vector2 steering = desiredVelocity - rigidbody.velocity;
        return steering.ShortenMagnitude(maxForce);
    }
    /// <summary>
    /// Arribe with fixed slowDownRadious
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <param name="objective"></param>
    /// <param name="maxSpeed"></param>
    public static Vector2 Arribe(Rigidbody2D rigidbody, Vector2 objective,  float maxSpeed, float maxForce, float stopRadious)
    {
        Vector2 desiredVelocity;
        Vector2 steering;
        //desde más pequeña es la desired vel mas rapido frenará.
        Vector2 distance = objective - rigidbody.position;
        float distanceMagnitude = distance.magnitude;
        float desiredSpeed = maxSpeed;

        if (distanceMagnitude < stopRadious)
        {
            desiredVelocity = Vector2.zero;
            steering = desiredVelocity - rigidbody.velocity; 
            return steering.ShortenMagnitude(maxForce);
        }

        if (distanceMagnitude < defaultArribeRadious)
        {
            float percentToArribe = distanceMagnitude / defaultArribeRadious; // 0-1
            desiredSpeed = maxSpeed * percentToArribe;

            desiredVelocity = (distance / distanceMagnitude) * desiredSpeed;
            steering = desiredVelocity - rigidbody.velocity;
            return steering.ShortenMagnitude(maxForce);
        }
        else
            return Seek(rigidbody, objective, maxSpeed, maxForce);
    }
    public static Vector2 Arribe(Rigidbody2D rigidbody, Vector2 objective, float maxSpeed, float maxForce, float slowDownRadious, float arribeRadious)
    {
        //desde más pequeña es la desired vel mas rapido frenará.
        Vector2 distance = objective - rigidbody.position;
        float distanceMagnitude = distance.magnitude;
        float desiredSpeed = maxSpeed;
        Vector2 desiredVelocity;
        Vector2 steering;
        if (distanceMagnitude < arribeRadious)
        {
            desiredVelocity = Vector2.zero;
            steering = desiredVelocity - rigidbody.velocity;
            return (steering);
        }

        if (distanceMagnitude < slowDownRadious)
        {
            float percentToArribe = distanceMagnitude / slowDownRadious;
            desiredSpeed = maxSpeed * percentToArribe;

            desiredVelocity = (distance / distanceMagnitude) * desiredSpeed;
            steering = desiredVelocity - rigidbody.velocity;
            return (steering);
        }
        else
            return Seek(rigidbody, objective, maxSpeed, maxForce);
    }

    static Vector2 GetTheNormalPointOfSegmentFromPoint(Vector2 point, Vector2 a, Vector2 b, out Vector2 segmentDirection)
    {
        Vector2 ap = point - a;
        Vector2 ab = b - a;
        segmentDirection = ab.normalized;

        float scalarProyection = Vector2.Dot(ap, segmentDirection);

        if (scalarProyection < 0 || scalarProyection * scalarProyection > ab.sqrMagnitude)
        {
            if (Vector2.Distance(point, a) < Vector2.Distance(point, b))
            {
                return a;
            }
            return b;
        }
        Vector2 normalPoint = a + (segmentDirection * scalarProyection);
        return normalPoint;
    }
    
}
