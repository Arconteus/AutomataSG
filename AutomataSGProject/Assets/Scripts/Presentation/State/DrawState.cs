using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class DrawState : MonoBehaviour
{
    [Header("Options")]
    public int Subdivision = 40;
    public float Radius = 0.5f;
    public float RelationBetExtIntRad = 0.8f;
    public float Width = 0.03f;
    public Color32 Color = new Color32(255,125,173,255);
    [Header("Game Object")]
    public LineRenderer _border;
    public LineRenderer _initial;
    public LineRenderer _final;
    void Start()
    {
        this.Config(this._border);
        this.Config(this._final);
        this.Config(this._initial);
        this.UpdateBorders();
    }
    void Update()
    {
        this.Config(this._border);
        this.Config(this._final);
        this.Config(this._initial);
    }
    private void Config(LineRenderer input)
    {
        input.startColor = Color;
        input.endColor = Color;
        input.startWidth = Width;
        input.endWidth = Width;
    }
    private void UpdateBorders()
    {
        float angleStep = 2f * Mathf.PI / this.Subdivision;
        _border.positionCount = this.Subdivision;
        for (int iteration = 0; iteration < this.Subdivision; iteration++)
        {
            float xPos = this.Radius * Mathf.Cos(angleStep * iteration);
            float yPos = this.Radius * Mathf.Sin(angleStep * iteration);
            Vector3 iterationVector = new Vector3(xPos, yPos, 0f);
            _border.SetPosition(iteration, iterationVector);
        }

        _final.positionCount = this.Subdivision;
        for (int iteration = 0; iteration < this.Subdivision; iteration++)
        {
            float xPos = this.Radius * this.RelationBetExtIntRad * Mathf.Cos(angleStep * iteration);
            float yPos = this.Radius * this.RelationBetExtIntRad * Mathf.Sin(angleStep * iteration);
            Vector3 iterationVector = new Vector3(xPos, yPos, 0f);
            _final.SetPosition(iteration, iterationVector);
        }
    }
}
