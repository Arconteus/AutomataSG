using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectIndicador : MonoBehaviour
{
    [Header("Game Object References")]
    public TextMeshProUGUI Previous;
    public TextMeshProUGUI Current;
    public GameObject CurrentState;
    public GameObject PreviousState;
    [Header("PrefabReference")]
    public GameObject prefabState;
    [Header("Parameters")]
    public float TimeBetwenClics = 0.2f;
    public float TimeSinceLastClic = 100;
    // variables de control privadas

    //================================================
    // Funciones de unity
    //================================================
    public void Start()
    {
        DFA.SetInitialState(this.CurrentState.GetComponent<StateController>().StateReference);
    }
    void Update()
    {
        this.NameUpdate();
        this.TimeSinceLastClic += Time.deltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            this.LeftClick();
        }
        if(Input.GetMouseButtonDown(1))
        {
            this.RightClick();
        }
    }
    //================================================
    // Funciones de control
    //================================================
    private void LeftClick()
    {
        this.TryCreateState();
    }
    private void RightClick()
    {
        this.TrySelectState();
    }

    //================================================
    // Funciones modificadas
    //================================================
    private void TrySelectState()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (this.CurrentState != hit.collider.gameObject)
            {
                this.PreviousState = this.CurrentState;
            }
            this.CurrentState = hit.collider.gameObject;
        }
        else
        {
            this.CurrentState = null;
            this.PreviousState = null;
        }
    }
    private void TryCreateState()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) return;
        if (this.TimeSinceLastClic < this.TimeBetwenClics)
        {
            GameObject last = Instantiate(this.prefabState);
            last.transform.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 5);
        }
        this.TimeSinceLastClic = 0;
    }
    private void NameUpdate()
    {
        if (CurrentState != null)
        {
            Current.text = CurrentState.name;
        }
        else
        {
            Current.text = "-";
        }
        if (PreviousState != null)
        {
            Previous.text = PreviousState.name;
        }
        else
        {
            Previous.text = "-";
        }
    }
}
