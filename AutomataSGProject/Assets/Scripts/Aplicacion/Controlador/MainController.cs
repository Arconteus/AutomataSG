using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SelectIndicador : MonoBehaviour
{
    [Header("Game Object References")]
    public GameObject OriginState;
    public GameObject TargetState;
    public GameObject MouseTraker;
    public GameObject Block;
    [Header("PrefabReference")]
    public GameObject prefabState;
    public GameObject prefabTransition;
    [Header("Parameters")]
    public float TimeBetwenClics = 0.2f;
    public float TimeSinceLastClic = 100;
    [Header("Control")]
    public bool RightClickWasPress = false;
    public char SymbolTransition = ' ';
    public GameObject TempStateSlot = null;
    //================================================
    // Funciones de unity
    //================================================
    public void Start()
    {
        GameObject FirstState = CreateState(new Vector3(-7.5f, 0, 0));
        DFA.InitialState.Set(FirstState.GetComponent<StateController>().StateReference);
        FirstState.GetComponent<MoveWorldObject>().enabled = false;
    }
    void Update()
    {
        this.TimeSinceLastClic += Time.deltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            if(!this.Block.activeSelf) this.LeftClickDown();
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (!this.Block.activeSelf) this.RightClickDown();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            if (!this.Block.activeSelf) this.RightClickUp();
        }
    }
    //================================================
    // Funciones de control
    //================================================
    private void LeftClickDown()
    {
        this.TryCreateState();
    }
    private void RightClickDown()
    {
        this.TryStartTransition();
        this.RightClickWasPress = true;
    }
    private void RightClickUp()
    {
        this.TryEndTransition();
        this.RightClickWasPress = false;
    }

    //================================================
    // Funciones modificadas
    //================================================
    private void TryStartTransition()
    {
        if (!this.RightClickWasPress)
        {
            string itemTag = string.Empty;
            RaycastHit2D item = this.CastRay2D();
            if (item.collider != null)
            {
                itemTag = item.collider.gameObject.tag;
            }
            if ((item.collider != null) && (itemTag == "State"))
            {
                this.OriginState = item.collider.gameObject;
                this.TargetState = null;
                this.TempStateSlot = Instantiate(this.prefabTransition);
                this.TempStateSlot.GetComponent<TransitionController>().Origin = this.OriginState.transform;
            }
            else
            {
                this.TargetState = null;
                this.OriginState = null;
            }
        }
    }
    private void TryEndTransition()
    {
        if (this.OriginState != null)
        {
            string itemTag = string.Empty;
            RaycastHit2D item = this.CastRay2D();
            if (item.collider != null)
            {
                itemTag = item.collider.gameObject.tag;
            }
            if ((item.collider != null) && (itemTag == "State"))
            {
                this.TargetState = item.collider.gameObject;
                this.TempStateSlot.GetComponent<TransitionController>().Target = this.TargetState.transform;
                this.TempStateSlot.GetComponent<TransitionController>().Rename();
                this.TempStateSlot = null;
            }
            else
            {
                this.OriginState = null;
                this.TargetState = null;
                Destroy(this.TempStateSlot);
                this.TempStateSlot = null;
            }
        }
        else
        {
            this.OriginState = null;
            this.TargetState = null;
        }
    }

    private void TryCreateState()
    {
        RaycastHit2D hit = this.CastRay2D();
        if (hit.collider != null) return;
        if (this.TimeSinceLastClic < this.TimeBetwenClics)
        {
            CreateState(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10));
        }
        this.TimeSinceLastClic = 0;
    }

    public GameObject CreateState(Vector3 input)
    {
        GameObject LastState = Instantiate(this.prefabState);
        LastState.transform.position = input;
        return LastState;
    }

    private RaycastHit2D CastRay2D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D output = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        return output;
    }
}
