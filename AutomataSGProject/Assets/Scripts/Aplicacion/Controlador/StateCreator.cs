using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCreator : MonoBehaviour
{
    [Header("Game Object References")]
    public GameObject Block;
    [Header("PrefabReference")]
    public GameObject prefabState;
    [Header("Parameters")]
    public float TimeBetwenClics = 0.2f;
    public float TimeSinceLastClic = 100;
    //================================================
    // Funciones de unity
    //================================================
    public void Start()
    {
        GameObject FirstState = CreateState(new Vector3(-7.5f, 0, 0));
        DFA.InitialState.Set(FirstState.GetComponent<StateController>().StateReference);
    }
    void Update()
    {
        this.TimeSinceLastClic += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (!this.Block.activeSelf) this.LeftClickDown();
        }
    }
    //================================================
    // Funciones de control
    //================================================
    private void LeftClickDown()
    {
        this.TryCreateState();
    }

    //================================================
    // Funciones modificadas
    //================================================

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
