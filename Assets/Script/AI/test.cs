using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    MachineState _MachineState;
    Jugar _Jugar;
    Dormir _Dormir;
    Comer _Comer;
    WCing _WCing;
    // Start is called before the first frame update
    void Start()
    {
        _MachineState =GetComponent<MachineState>();
        _Jugar = GetComponent<Jugar>();
        _Dormir = GetComponent<Dormir>();
        _Comer = GetComponent<Comer>();
        _WCing = GetComponent<WCing>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            _MachineState.ChangeState(_Jugar);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _MachineState.ChangeState(_Dormir);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _MachineState.ChangeState(_Comer);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _MachineState.ChangeState(_WCing);
        }
    }
}
