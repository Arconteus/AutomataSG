using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUpdate : MonoBehaviour
{
    [Header("Variables de Linea")]
    public Color color;
    public float width;
    //============================
    // Variables de control
    //============================
    private GameObject origin;
    private GameObject target;
    private LineRenderer lr;

    public void Start()
    {
        this.lr = GetComponent<LineRenderer>();
        this.lr.positionCount = 2;
        this.lr.startColor = this.color;
        this.lr.endColor = this.color;
        this.lr.startWidth = width;
        this.lr.endWidth = width;
    }
    public void setStartState(GameObject origin)
    {
        this.origin = origin;
    }
    public void setTargetState(GameObject target)
    {
        this.target = target;
    }
    public void Update()
    {
        if (origin != null)
        {
            this.lr.SetPosition(0, this.origin.transform.position);
        }
        if(target != null)
        {
            this.lr.SetPosition(1,this.target.transform.position);
            this.name = "T:"+this.origin.name+":"+this.target.name;
        }
        else
        {
            this.lr.SetPosition(1,Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
