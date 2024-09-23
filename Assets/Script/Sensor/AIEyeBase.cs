using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEyeBase : MonoBehaviour
{
    protected int count = 0;
    // protected Collider[] colliders = new Collider[10];
    public DataView mainDataView = new DataView();
    #region Rate
    protected int index = 0;
    protected float[] arrayRate;
    protected int bufferSize = 10;
    public float randomWaitScandMin = 1;
    public float randomWaitScandMax = 1;
    protected float Framerate = 0;
    #endregion
    public ViewObject _Owner { get; set; }
    public bool IsDrawGizmo = false;
    //public ViewObject ScanViewObj;
    public List<ViewObject> ScanViewObjs = new List<ViewObject>(); // Cambiado a lista

    public virtual void LoadComponent()
    {
        _Owner = GetComponent<ViewObject>();

        mainDataView.Owner = _Owner;
        Framerate = 0;
        index = 0;
        arrayRate = new float[bufferSize];
        for (int i = 0; i < arrayRate.Length; i++)
        {
            arrayRate[i] = (float)UnityEngine.Random.Range(randomWaitScandMin, randomWaitScandMax);
        }
    }
    public virtual void UpdateScan()
    {
        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            Scan();
            Framerate = 0;
        }
        Framerate += Time.deltaTime;
    }
    public virtual void Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, mainDataView.Distance, mainDataView.Scanlayers);

        count = colliders.Length;
        //ScanViewObj = null;
        ScanViewObjs.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (this.IsNotIsThis(this.gameObject, obj))
            {
                ViewObject ScanObj = obj.GetComponent<ViewObject>();
                if (ScanObj != null &&
                obj.activeSelf &&
                ScanObj.IsCantView &&
                 mainDataView.IsInSight(obj.transform))
                {
                    //ScanViewObj = ScanObj;
                    ScanViewObjs.Add(ScanObj);

                }
            }
        }
    }
    public virtual bool IsNotIsThis(GameObject obj1, GameObject obj2)
    {

        return (obj1.GetInstanceID() != obj2.GetInstanceID());
    }
}