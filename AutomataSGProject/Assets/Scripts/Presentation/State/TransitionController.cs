using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [Header("State Reference")]
    public Transform Origin;
    public Transform Target;
    public GameObject Arrow;
    [Header("LineRenderer")]
    public LineRenderer line;
    [Header("Parameters")]
    public float widthLine = 0.2f;
    public float ArrowDistance = 0.5f;
    public Color defaultColor;
    public Color transitionColor;
    public Color SelectedColor;
    //===================================
    // Variables de control
    //===================================
    Vector3 Correction;
    Vector3 tempVector;
    //===================================
    // Funciones de unity
    //===================================
    public void Start()
    {
        this.Config();
        this.LineaUpdate();
        this.ArrowUpdate();
    }
    public void Update()
    {
        this.LineaUpdate();
        this.ArrowUpdate();
    }
    //===============================================
    // Funciones custom
    //===============================================
    public void Rename()
    {
        string Qname1 = this.Origin.transform.gameObject.name;
        string Qname2 = this.Target.transform.gameObject.name;
        this.name = Qname1+"-"+Qname2;

    }
    //===============================================
    // Funciones privadas
    //===============================================
    private void Config()
    {
        this.line.positionCount = 2;
        this.line.startColor = this.defaultColor;
        this.line.endColor = this.defaultColor;
        this.line.startWidth = this.widthLine;
        this.line.endWidth = this.widthLine;
        this.Arrow.GetComponent<SpriteRenderer>().color = this.defaultColor;
    }
    private void LineaUpdate()
    {

        if(this.Target != null)
        {
            this.tempVector = this.Target.position;
        }
        else
        {
            this.tempVector = 
                Camera.main.ScreenToWorldPoint(Input.mousePosition)+Vector3.forward*10;
        }
        this.transform.position =
            (this.Origin.position + this.tempVector - Correction.normalized * (ArrowDistance / 2)) / 2;
        this.Correction = this.tempVector - this.Origin.position;
        this.line.SetPosition(0, Origin.position + this.Correction.normalized * 0.5f);
        this.line.SetPosition(1, this.tempVector - this.Correction.normalized * 0.5f);
    }
    private void ArrowUpdate()
    {
        this.Arrow.transform.position = this.tempVector - (this.Correction.normalized * ArrowDistance);
        float angle = Mathf.Atan2(Correction.y, Correction.x) * Mathf.Rad2Deg - 90f;
        this.Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
