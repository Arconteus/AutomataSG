using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class TransitionCreator : MonoBehaviour
{
    [Header("Text Reference")]
    public TextMeshProUGUI q0;
    public TextMeshProUGUI q1;
    [Header("Prefab Reference")]
    public GameObject lRender;
    //==========================
    // Control Variables
    //==========================
    GameObject last;
    GameObject first;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.first = this.last;
            this.last = CastRay();
        }
        this.q0q1Text();
        this.createLineRender();

    }
    private void createLineRender()
    {
        if(this.first != null)
        {
            if(this.last != null)
            {
                GameObject line = Instantiate(this.lRender);
                line.GetComponent<TransitionUpdate>().setStartState(this.first);
                line.GetComponent<TransitionUpdate>().setTargetState(this.last);
                this.first = null;
                this.last = null;
            }
        }
    }
    private void q0q1Text()
    {
        if (this.last != null)
        {
            this.q1.text = this.last.name;
        }
        else
        {
            this.q1.text = "-";
        }
        if (this.first != null)
        {
            this.q0.text = this.first.name;
        }
        else
        {
            this.q0.text = "-";
        }
    }

    private GameObject CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            return hit.collider.gameObject;
        }
        return null;
    }

}