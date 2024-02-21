using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject blockPanel;
    [Header("Settings")]
    public float maxScreenSize = 7f;
    public float minScreenSize = 3f;
    public bool invertZoom = true;
    void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            bool run = true;
            float ScrollDelta = Input.mouseScrollDelta.y;
            if (blockPanel.activeSelf)
            {
                return;
            }
            if(invertZoom)
            {
                ScrollDelta *= -1;
            }
            if ((Camera.main.orthographicSize + ScrollDelta) > maxScreenSize)
            {
                run = false;
                Debug.LogError(" Zoom: " + Camera.main.orthographicSize + " Scroll; " + ScrollDelta);
            }
            if ((Camera.main.orthographicSize + ScrollDelta) < minScreenSize)
            {
                run = false;
                Debug.LogError(" Zoom: " + Camera.main.orthographicSize + " Scroll; " + ScrollDelta);
            }
            if (run)
            {
                Camera.main.orthographicSize += ScrollDelta;
            }
        }
    }
}
