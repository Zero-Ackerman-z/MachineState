using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comiendo : StateWait
{

    // Start is called before the first frame update
    void Awake()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {
        stateType = StateType.Comiendo;
        base.LoadComponent();
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Jugando Enter ");
    }
    public override void Execute()
    {
        base.Execute();
        if (!WaitTime)
        {
            _MachineState.ActiveState(GetRandomStateType());
        }

        Debug.Log("Jugando Execute ");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Jugando Exit ");
    }
}
