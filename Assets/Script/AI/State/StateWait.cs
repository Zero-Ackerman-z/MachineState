using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class StateWait : StateBase
{

    protected Coroutine coroutine;
    protected bool WaitTime = false;
    public float TimeWait;
    public TMPro.TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        this.LoadComponent();
    }
    public override void LoadComponent()
    {

        base.LoadComponent();

    }
    public override void Enter()
    {
        textMeshPro.gameObject.SetActive(true);
        coroutine = StartCoroutine(RunWait());
        base.Enter();
    }
    IEnumerator RunWait()
    {
        WaitTime = true;
        while (TimeWait>0)
        {
            TimeWait--;
            if(textMeshPro!=null)
                textMeshPro.text=TimeWait.ToString("00");
            yield return new WaitForSeconds(1f);
        }
       
        
        WaitTime = false;
        StopCoroutine(coroutine);
        coroutine=null;
    }
    public override void Execute()
    {

        base.Execute();
    }
    public override void Exit()
    {
        textMeshPro.gameObject.SetActive(false);
        base.Exit();
    }
}
