using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected MachineState _MachineState;
    public StateType stateType;
    public StateNode stateNode;
    public virtual void LoadComponent()
    {
        _MachineState = GetComponent<MachineState>();
    }
    public virtual void Enter()
    {

    }
    public virtual void Execute()
    {

    }
    public virtual void Exit()
    {

    }
    public StateType GetRandomStateType()
    {
        StateType[] values = (StateType[])System.Enum.GetValues(typeof(StateType));
        List<StateType> possibleValues = new List<StateType>();

        foreach (StateType value in values)
        {
            if (value != stateType)
            {
                possibleValues.Add(value);
            }
        }
        int randomIndex = Random.Range(0, possibleValues.Count);
        return possibleValues[randomIndex];
    }
}
