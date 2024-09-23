using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugando : StateWait
{
    void Awake()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        stateType = StateType.Jugando;
        base.LoadComponent();
    }
    public override void Enter()
    {
        base.Enter();
        stateNode = StateNode.MoveTo;
        Debug.Log("Jugando Enter ");
    }
    public override void Execute()
    {
        base.Execute();
        GetComponent<TestAIEye>().UpdateScan();

        Transform detectedToy = GetComponent<TestAIEye>().ScanViewObjs.Find(obj => obj != null)?.transform;

        if (detectedToy != null)
        {
            stateNode = StateNode.MoveTo;
            base.MoveToTarget(detectedToy);
        }
        else
        {
            switch (stateNode)
            {
                case StateNode.MoveTo:
                    base.MoveToPlace();
                    float distancia = (transform.position - place.position).magnitude;
                    if (distancia < 1)
                    {
                        stateNode = StateNode.StartStay;
                    }
                    break;
                case StateNode.StartStay:
                    StartCoroutineWait();
                    stateNode = StateNode.Stay;
                    break;


                case StateNode.Stay:
                    WanderAround();
                    if (!WaitTime)
                    {
                        _MachineState.ActiveState(GetRandomStateType());
                        return;
                    }
                    Debug.Log("Jugando Execute ");
                    break;
                case StateNode.Finish:
                    break;
                default:
                    break;
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        stateNode = StateNode.MoveTo;
        Debug.Log("Jugando Exit ");
    }
}
