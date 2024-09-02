using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : StateBase
{
    protected SteeringBehavior _SteeringBehavior;
    public Transform place;
    // Start is called before the first frame update
    public override void LoadComponent()
    {
        _SteeringBehavior = GetComponent<SteeringBehavior>();
        base.LoadComponent();
    }
}
