using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestAIEye : AIEyeBase
{
    public DataView DataViewAttack = new DataView();
    public DataView DataViewFire = new DataView();
    private StateMove stateMove; // Referencia al estado de movimiento
    public Transform detectedToy; // Variable para almacenar el juguete detectado

    private void Start()
    {
        LoadComponent();
        stateMove = GetComponent<StateMove>(); // Obtén la referencia al componente StateMove

    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }
    public override void UpdateScan()
    {
        base.UpdateScan();
        detectedToy = null;
        foreach (var scanObj in ScanViewObjs) // Iterar sobre los objetos detectados
        {
            if (scanObj != null)
            {
                // Mover hacia el objeto detectado
                stateMove.MoveToTarget(scanObj.transform);

                // Verificar si está a la vista
                DataViewAttack.IsInSight(scanObj.AimOffset);
                DataViewFire.IsInSight(scanObj.AimOffset);
                detectedToy = scanObj.transform;
                break; // Detener el loop una vez encontrado un objeto válido
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