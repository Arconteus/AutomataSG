using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorUpdate : MonoBehaviour
{
    [Header("Color settings")]
    public Color defaultColor;
    public Color hoverColor;
    public Color DrahColor;
    public Color selectColor;
    public Color currentStateColor;
    public Color previousStateColor;
    [Header("Sprites References")]
    public DrawState _state;
    //=============================================
    // variables de control
    //=============================================
    private bool isHover = false;
    private bool isDrag = false;
    public void Update()
    {
        if (this.isDrag)
        {
            this._state.Color = this.DrahColor;
        }
        else if (this.isHover)
        {
            this._state.Color = this.hoverColor;
        }
        else
        {
            this._state.Color = this.defaultColor;
        }
    }
    public void OnMouseEnter()
    {
        this.isHover = true;
    }
    public void OnMouseExit()
    {
        this.isHover = false;
    }
    public void OnMouseDrag()
    {
        this.isDrag = true;
    }
    public void OnMouseUp()
    {
        this.isDrag = false;
    }
}
