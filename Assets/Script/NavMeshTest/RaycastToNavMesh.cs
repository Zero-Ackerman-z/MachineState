using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RaycastToNavMesh : MonoBehaviour
{
    public Camera mainCamera;  // La c�mara desde la que se lanza el raycast
    public float maxRaycastDistance = 100f;  // Distancia m�xima del raycast
    public float sampleDistance = 1.0f;  // Radio de b�squeda para NavMesh.SamplePosition
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
            // Llamamos a la funci�n para obtener la posici�n en el NavMesh bajo el cursor
            Vector3 navMeshPosition;
            if (GetNavMeshPositionUnderCursor(out navMeshPosition))
            {
                Debug.Log("Posici�n en el NavMesh: " + navMeshPosition);
                agent.SetDestination(navMeshPosition);
                cursor.transform.position = navMeshPosition + Vector3.up;
                StartCoroutine(ShowCursor());

                // Puedes hacer lo que quieras con esta posici�n, como mover un agente hacia ella
            }
            else
            {
                Debug.LogWarning("No se encontr� una posici�n en el NavMesh bajo el cursor.");
            }
        }
    }

    // Funci�n que devuelve la posici�n en el NavMesh bajo el cursor de la pantalla
    bool GetNavMeshPositionUnderCursor(out Vector3 navMeshPosition)
    {
        navMeshPosition = Vector3.zero;

        // Obtenemos la posici�n del cursor en pantalla
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Realizamos un Raycast en el escenario
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            // Intentamos encontrar la posici�n en el NavMesh m�s cercana al punto de impacto
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(hit.point, out navMeshHit, sampleDistance, NavMesh.AllAreas))
            {
                navMeshPosition = navMeshHit.position;
                return true;  // Se encontr� una posici�n en el NavMesh
            }
        }

        return false;  // No se encontr� una posici�n en el NavMesh
    }
}
