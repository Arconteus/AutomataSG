using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StateController : MonoBehaviour
{
    public static int StateCount { get; private set; } = 0;
    [Header("GameObject reference")]
    public GameObject FinalSkin;
    public GameObject InputSkin;
    public TextMeshPro Etiqueta;
    [Header("Parameters")]
    public string StateReference;
    public float TimeBetwenClics = 0.2f;
    public float TimeSinceLastLeftClick = 100;
    //============================================
    // Funciones de Unity
    //============================================
    public void Awake()
    {
        DFA.States.Add(StateCount.ToString());
        this.StateReference = StateCount.ToString();
        this.name = "q" + this.StateReference;
        StateCount++;
    }
    public void Update()
    {
        this.UpdateState();
        this.TimeSinceLastLeftClick += Time.deltaTime;
    }
    public void OnMouseDown()
    {
        if(this.TimeSinceLastLeftClick < this.TimeBetwenClics)
        {
            this.SwapFinalState();
        }
        this.TimeSinceLastLeftClick = 0;
    }
    //============================================
    // Funciones Custom
    //============================================
    
    public void UpdateState()
    {
        // Nombre
        this.Etiqueta.text = this.gameObject.name;
        // Skin del nodo
        if (DFA.FinalSates.List.Contains(this.StateReference))
        {
            this.FinalSkin.SetActive(true);
        }
        else
        {
            this.FinalSkin.SetActive(false);
        }
        if (DFA.Simulation.CurrentState == this.StateReference)
        {
            this.InputSkin.SetActive(true);
        }
        else
        {
            this.InputSkin.SetActive(false);
        }
    }
    public void SwapFinalState()
    {
        if(!DFA.FinalSates.List.Contains(this.StateReference) )
        {
            DFA.FinalSates.Add(this.StateReference);
        }
        else
        {
            DFA.FinalSates.Remove(this.StateReference);
        }
    }
}
