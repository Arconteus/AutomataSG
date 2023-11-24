using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWorldObject : MonoBehaviour
{
    Vector2 differences;
    public void Start()
    {
        this.differences = Vector2.zero;
    }
    private void OnMouseDown()
    {
        this.differences=
            (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)
            -(Vector2)transform.position;
    }
    private void OnMouseDrag()
    {
        this.transform.position=
            (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)
            -this.differences;
    }
}
