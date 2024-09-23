using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DataView : DataViewBase
{
    public LayerMask occlusionlayers;
    [Header("InsideObject")]
    public bool InsideObject = true;
    public DataView()
    { }
    public override bool IsInSight(Transform AimOffset)
    {
        if (AimOffset == null) return false;

        Vector3 origin = Owner.AimOffset.position;
        Vector3 dest = AimOffset.position;
        Vector3 direcction = dest - origin;

        if (direcction.magnitude > distance)
            return false;
        if (dest.y < -(height + Owner.transform.position.y) || dest.y > (height + Owner.transform.position.y))
        {
            return false;
        }
        direcction.y = 0;
        float deltaAngle = Vector3.Angle(direcction.normalized, Owner.AimOffset.forward);
        if (deltaAngle > angleFOV)
        {
            return false;
        }
        if (Physics.Linecast(origin, dest, occlusionlayers) && InsideObject)
        {
            return false;
        }
        meshColor = Color.yellow;
        meshColor.a = 0.4f;
        return true;
    }
    public override void OnDrawGizmos()
    {
        if (!IsDrawGizmo) return;
        base.OnDrawGizmos();
    }
}
