using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    public List<Transform> paths = new List<Transform>();
    
    public bool DrawGizmo = false;
    public Color ColorGizmos;
 
    private void OnDrawGizmos()
    {
        if (!DrawGizmo) return;
         
        Gizmos.color = ColorGizmos;
        foreach(Transform t in paths)
        {
            Gizmos.DrawSphere(t.position, 0.2f);
        }
    }
}
