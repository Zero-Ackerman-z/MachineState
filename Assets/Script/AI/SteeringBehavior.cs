using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    // Atributo que representa el objetivo al que se desea acercar
    //public Transform Target;

    // Velocidad máxima del objeto
    public float maxSpeed = 10f;

    // Fuerza máxima de la aceleración
    public float maxForce = 5f;

    // Velocidad actual del objeto
    public Vector3 velocity;
    // Radio de desaceleración
    public float slowingRadius = 5f;
    private int currentWaypointIndex = 0;

    private Vector3 CalculateSteeringForce(Vector3 desired)
    {
        // Calcula la fuerza de dirección
        Vector3 steering = desired - velocity;
        return Vector3.ClampMagnitude(steering, maxForce);
    }

    public void UpdatePosition()
    {
        transform.position += velocity * Time.deltaTime;
    }
    public void  ClampMagnitude(Vector3 steeringForce)
    {
        // Aplica la fuerza a la velocidad
        velocity = Vector3.ClampMagnitude(velocity + steeringForce * Time.deltaTime, maxSpeed);

    }
    // Función de Seek que calcula la fuerza de dirección hacia el objetivo
    public Vector3 Seek(Transform target)
    {
        Vector3 desired = (target.position - transform.position).normalized * maxSpeed;
        return CalculateSteeringForce(desired);
    }
    public Vector3 Flee(Transform target)
    {
        // Calcula el vector deseado alejándose del objetivo
        Vector3 desired = (transform.position - target.position).normalized * maxSpeed;
        return CalculateSteeringForce(desired);
    }
    public Vector3 Arrive(Transform target)
    {
        // Calcula el vector hacia el objetivo
        Vector3 desired = target.position - transform.position;
        float distance = desired.magnitude;

        if (distance < slowingRadius)
        {
            desired = desired.normalized * maxSpeed * (distance / slowingRadius);
        }
        else
        {
            desired = desired.normalized * maxSpeed;
        }

        return CalculateSteeringForce(desired);
    }
    private Vector3 PredictFuturePosition(Transform target, Vector3 targetVelocity)
    {
        Vector3 distance = target.position - transform.position;
        float updatesAhead = distance.magnitude / maxSpeed;
        return target.position + targetVelocity * updatesAhead;
    }
    // Evade: huir de un objetivo prediciendo su posición futura
    public Vector3 Evade(Transform target, Vector3 targetVelocity)
    {
        Vector3 futurePosition = PredictFuturePosition(target, targetVelocity);
        Vector3 desired = transform.position - futurePosition;
        desired.Normalize();
        desired *= maxSpeed;

        return CalculateSteeringForce(desired);
    }
    // Pursuit: perseguir a un objetivo prediciendo su posición futura
    public Vector3 Pursuit(Transform target, Vector3 targetVelocity)
    {
        Vector3 futurePosition = PredictFuturePosition(target, targetVelocity);
        Vector3 desired = futurePosition - transform.position;
        desired.Normalize();
        desired *= maxSpeed;

        return CalculateSteeringForce(desired);
    }

    // Wander: deambular aleatoriamente dentro de un radio
    public Vector3 Wander()
    {
        float wanderRadius = 2f;        // Radio de deambulación
        float wanderDistance = 5f;      // Distancia desde el centro del círculo de deambulación
        float wanderJitter = 1f;        // Cantidad de variación aleatoria en la dirección

        // Desplaza la posición de la dirección actual
        Vector3 circleCenter = velocity.normalized * wanderDistance;
        Vector3 displacement = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * wanderRadius * wanderJitter;

        Vector3 wanderForce = circleCenter + displacement;
        return Vector3.ClampMagnitude(wanderForce, maxForce);
    }

    // Path Following: seguir un camino predefinido de puntos de referencia
    public Vector3 PathFollowing(Vector3[] path, ref int currentWaypointIndex)
    {
        if (currentWaypointIndex >= path.Length)
            return Vector3.zero;

        Vector3 target = path[currentWaypointIndex];
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= path.Length)
                currentWaypointIndex = path.Length - 1;
        }

        // Convierte el target a Transform para usar con Seek
        Transform targetTransform = new GameObject().transform;
        targetTransform.position = target;
        return Seek(targetTransform);
    }

    // Obstacle Avoidance: evitar obstáculos en el camino
    public Vector3 ObstacleAvoidance(LayerMask obstacleLayer)
    {
        float avoidDistance = 5f;
        float avoidForce = 10f;

        if (Physics.Raycast(transform.position, velocity.normalized, out RaycastHit hit, avoidDistance, obstacleLayer))
        {
            Vector3 avoidDirection = Vector3.Reflect(velocity.normalized, hit.normal);
            return avoidDirection * avoidForce;
        }

        return Vector3.zero;
    }

    // Separation: separarse de otros agentes cercanos
    public Vector3 Separation(Transform[] neighbors)
    {
        Vector3 separationForce = Vector3.zero;
        int count = 0;

        foreach (Transform neighbor in neighbors)
        {
            float distance = Vector3.Distance(transform.position, neighbor.position);
            if (distance > 0 && distance < slowingRadius)
            {
                Vector3 fleeDirection = transform.position - neighbor.position;
                separationForce += fleeDirection.normalized / distance;
                count++;
            }
        }

        if (count > 0)
        {
            separationForce /= count;
            separationForce = separationForce.normalized * maxForce;
        }

        return separationForce;
    }

    // Cohesion: moverse hacia el centro de los agentes cercanos
    public Vector3 Cohesion(Transform[] neighbors)
    {
        Vector3 centerOfMass = Vector3.zero;
        int count = 0;

        foreach (Transform neighbor in neighbors)
        {
            centerOfMass += neighbor.position;
            count++;
        }

        if (count > 0)
        {
            centerOfMass /= count;
            Vector3 desired = centerOfMass - transform.position;
            desired.Normalize();
            desired *= maxSpeed;
            return CalculateSteeringForce(desired);
        }

        return Vector3.zero;
    }

    // Alignment: alinearse con la dirección de movimiento de otros agentes cercanos
    public Vector3 Alignment(Transform[] neighbors)
    {
        Vector3 averageVelocity = Vector3.zero;
        int count = 0;

        foreach (Transform neighbor in neighbors)
        {
            averageVelocity += neighbor.GetComponent<SteeringBehavior>().velocity;
            count++;
        }

        if (count > 0)
        {
            averageVelocity /= count;
            averageVelocity = averageVelocity.normalized * maxSpeed;
            return CalculateSteeringForce(averageVelocity);
        }

        return Vector3.zero;
    }

    //void Update()
    //{
    //    // Calcula la fuerza de dirección
    //    Vector3 steeringForce = Seek(Target);

    //    // Aplica la fuerza a la velocidad
    //    velocity = Vector3.ClampMagnitude(velocity + steeringForce * Time.deltaTime, maxSpeed);

    //    // Actualiza la posición del objeto
    //    transform.position += velocity * Time.deltaTime;
    //}
}
