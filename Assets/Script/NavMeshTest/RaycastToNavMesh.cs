using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RaycastToNavMesh : MonoBehaviour
{
    public Camera mainCamera;  // La cámara desde la que se lanza el raycast
    public float maxRaycastDistance = 100f;  // Distancia máxima del raycast
    public float sampleDistance = 1.0f;  // Radio de búsqueda para NavMesh.SamplePosition
    private NavMeshAgent agent;
    public GameObject cursor;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cursor.SetActive(false);
    }

    IEnumerator ShowCursor()
    {
        cursor.SetActive(true);
        yield return new WaitForSeconds(2f);
        cursor.SetActive(false);
    }
    void Update()
    {
        // Detectamos el clic del mouse izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            // Llamamos a la función para obtener la posición en el NavMesh bajo el cursor
            Vector3 navMeshPosition;
            if (GetNavMeshPositionUnderCursor(out navMeshPosition))
            {
                Debug.Log("Posición en el NavMesh: " + navMeshPosition);
                agent.SetDestination(navMeshPosition);
                cursor.transform.position = navMeshPosition + Vector3.up;
                StartCoroutine(ShowCursor());

                // Puedes hacer lo que quieras con esta posición, como mover un agente hacia ella
            }
            else
            {
                Debug.LogWarning("No se encontró una posición en el NavMesh bajo el cursor.");
            }
        }
    }

    // Función que devuelve la posición en el NavMesh bajo el cursor de la pantalla
    bool GetNavMeshPositionUnderCursor(out Vector3 navMeshPosition)
    {
        navMeshPosition = Vector3.zero;

        // Obtenemos la posición del cursor en pantalla
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Realizamos un Raycast en el escenario
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            // Intentamos encontrar la posición en el NavMesh más cercana al punto de impacto
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, sampleDistance, NavMesh.AllAreas))
            {
                navMeshPosition = navMeshHit.position;
                return true;  // Se encontró una posición en el NavMesh
            }
        }

        return false;  // No se encontró una posición en el NavMesh
    }
}
