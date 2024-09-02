using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateBase : MonoBehaviour
{
    protected MachineState _MachineState;
    public StateType stateType;
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
        // Obtiene todos los valores del enum StateType
        StateType[] values = (StateType[])System.Enum.GetValues(typeof(StateType));

        // Elige un índice aleatorio dentro del rango de valores
        int randomIndex = Random.Range(0, values.Length);

        // Devuelve el valor aleatorio
        return values[randomIndex];
    }
}
