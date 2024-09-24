using UnityEngine;
using UnityEngine.AI;

public class MoveAgent : MonoBehaviour
{
    public Transform target;  // La posici�n B (el destino) como un Transform (puede ser un objeto en la escena)
    private NavMeshAgent agent;
    public float sampleDistance = 2.0f;  // Radio para buscar la posici�n
    void Start()
    {
        // Obtenemos el componente NavMeshAgent del GameObject
        agent = GetComponent<NavMeshAgent>();

        // Si no tienes un target asignado manualmente, puedes asignar una posici�n fija en el c�digo
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
            result = hit.position;  // Asigna la posici�n encontrada al par�metro de salida
            return true;  // Se encontr� una posici�n v�lida en el NavMesh
        }

        result = Vector3.zero;  // Si no se encuentra una posici�n, se devuelve un Vector3.zero
        return false;  // No se encontr� una posici�n en el NavMesh
    }
    void Update()
    {
        // Si el destino cambia din�micamente, puedes seguir actualizando la posici�n destino en Update
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
