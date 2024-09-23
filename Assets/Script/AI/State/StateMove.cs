using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    protected SteeringBehavior _SteeringBehavior;
    public Transform place;
    public LayerMask obstacleLayer; 
    public KeyPath keyPath;
    public Transform sensor;
    private int currentWaypointIndex = 0; 
    //public Transform[] neighbors; 

    // Start is called before the first frame update
    public override void LoadComponent()
    {
        _SteeringBehavior = GetComponent<SteeringBehavior>();
        base.LoadComponent();
    }
    public virtual void MoveToPlace()
    {
        //  PathFollowing en lugar de Arrive
        //Vector3 pathForce = _SteeringBehavior.PathFollowing(keyPath, ref currentWaypointIndex);

        Vector3 arriveForce = _SteeringBehavior.Arrive(place);
        // Obtén la fuerza de evitar obstáculos
        Vector3 avoidanceForce = _SteeringBehavior.ObstacleAvoidance(obstacleLayer);

        // Suma ambas fuerzas
        Vector3 combinedForce = arriveForce + avoidanceForce;
        _SteeringBehavior.ClampMagnitude(combinedForce);


        // buscar un objetivo específico
        // Vector3 seekForce = _SteeringBehavior.Seek(targetTransform);
        // combinedForce += seekForce;

        //  huir de un objetivo específico
        // Vector3 fleeForce = _SteeringBehavior.Flee(targetTransform);
        // combinedForce += fleeForce;

        // evadir un objetivo con una velocidad objetivo
        // Vector3 evadeForce = _SteeringBehavior.Evade(targetTransform, targetVelocity);
        // combinedForce += evadeForce;

        // perseguir un objetivo con una velocidad objetivo
        // Vector3 pursuitForce = _SteeringBehavior.Pursuit(targetTransform, targetVelocity);
        // combinedForce += pursuitForce;

        //  moverse aleatoriamente
        // Vector3 wanderForce = _SteeringBehavior.Wander();
        // combinedForce += wanderForce;

        // separación de otros agentes
        // Vector3 separationForce = _SteeringBehavior.Separation(neighborsTransforms);
        // combinedForce += separationForce;

        // alineación con otros agentes
        // Vector3 alignmentForce = _SteeringBehavior.Alignment(neighborsTransforms);
        // combinedForce += alignmentForce;

        // cohesión con otros agentes
        // Vector3 cohesionForce = _SteeringBehavior.Cohesion(neighborsTransforms);
        // combinedForce += cohesionForce;

        // Actualiza la posición del objeto con la fuerza combinada
        _SteeringBehavior.UpdatePosition();
    }
    public virtual void WanderAround()
    {
        Vector3 wanderForce = _SteeringBehavior.Wander();
        _SteeringBehavior.ClampMagnitude(wanderForce);
        _SteeringBehavior.UpdatePosition();
    }
    public virtual void MoveToTarget(Transform target)
    {
        Vector3 arriveForce = _SteeringBehavior.Arrive(target);
        _SteeringBehavior.ClampMagnitude(arriveForce);
        _SteeringBehavior.UpdatePosition(); // Actualiza la posición
    }
    
}
