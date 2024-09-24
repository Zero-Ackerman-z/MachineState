using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestAIEye : AIEyeBase
{
    public DataView DataViewAttack = new DataView();
    public DataView DataViewFire = new DataView();
    private StateMove stateMove; 
    public Transform detectedToy; 
    private void Start()
    {
        LoadComponent();
        stateMove = GetComponent<StateMove>(); 

    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void UpdateScan()
    {
        base.UpdateScan();
        detectedToy = null;
        foreach (var scanObj in ScanViewObjs) 
        {
            if (scanObj != null)
            {
                stateMove.MoveToTarget(scanObj.transform);

                DataViewAttack.IsInSight(scanObj.AimOffset);
                DataViewFire.IsInSight(scanObj.AimOffset);
                detectedToy = scanObj.transform;
                break; 
            }
        }
    }

    private void Update()
    {
        base.UpdateScan();
    }
    private void OnValidate()
    {
        mainDataView.CreateMesh();
        DataViewAttack.CreateMesh();
        DataViewFire.CreateMesh();
    }
    private void OnDrawGizmos()
    {

        mainDataView.OnDrawGizmos();
        DataViewAttack.OnDrawGizmos();
        DataViewFire.OnDrawGizmos();

    }

}