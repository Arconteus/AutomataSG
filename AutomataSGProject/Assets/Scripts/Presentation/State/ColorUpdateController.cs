using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorUpdateController : MonoBehaviour
{
    [Header("Color settings")]
    public Color defaultColor;
    public Color hoverColor;
    public Color selectColor;
    [Header("Text References")]
    public TextMeshPro stateName;
    [Header("Sprites References")]
    public SpriteRenderer sprite;
    public SpriteRenderer finalSprite;
    public SpriteRenderer inputSprite;
    public void Update()
    {
        this.stateName.text = this.name;
        this.finalSprite.color = this.sprite.color;
        this.inputSprite.color = this.sprite.color;
    }
    public void OnMouseEnter()
    {
        this.sprite.color = this.hoverColor;
    }
    public void OnMouseExit()
    {
        this.sprite.color = this.defaultColor;
    }
    public void OnMouseDrag()
    {
        this.sprite.color = this.selectColor;
    }
}
