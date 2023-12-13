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
    [Header("Sprites References")]
    public SpriteRenderer sprite;
    public SpriteRenderer finalSprite;
    public SpriteRenderer inputSprite;
    //=============================================
    // variables de control
    //=============================================
    private bool isHover = false;
    private bool isDrag = false;
    public void Update()
    {
        this.finalSprite.color = this.sprite.color;
        this.inputSprite.color = this.sprite.color;
        if (this.isDrag)
        {
            this.sprite.color = this.DrahColor;
        }
        else if (this.isHover)
        {
            this.sprite.color = this.hoverColor;
        }
        else
        {
            this.sprite.color = this.defaultColor;
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
