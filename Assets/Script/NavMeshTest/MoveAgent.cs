using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public Transform target;  // La posición B (el destino) como un Transform (puede ser un objeto en la escena)
    private NavMeshAgent agent;
    public float sampleDistance = 2.0f;  // Radio para buscar la posición
    void Start()
    {
        // Obtenemos el componente NavMeshAgent del GameObject
        agent = GetComponent<NavMeshAgent>();

        // Si no tienes un target asignado manualmente, puedes asignar una posición fija en el código
        if (target != null)
        {
            // Establece el destino al que el agente debe ir
            agent.SetDestination(target.position);
        }
    }
    bool GetNearestPointOnNavMesh(Vector3 targetPosition, out Vector3 result)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, sampleDistance, NavMesh.AllAreas))
        {
            result = hit.position;  // Asigna la posición encontrada al parámetro de salida
            return true;  // Se encontró una posición válida en el NavMesh
        }

        result = Vector3.zero;  // Si no se encuentra una posición, se devuelve un Vector3.zero
        return false;  // No se encontró una posición en el NavMesh
    }
    void Update()
    {
        // Si el destino cambia dinámicamente, puedes seguir actualizando la posición destino en Update
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
