using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutomataSettings : MonoBehaviour
{
    [Header("Prefab Reference")]
    public GameObject statePrefab;
    private int count;
    public void Start()
    {
        count = 0;
    }
    public void createState()
    {
        GameObject stateQn = Instantiate(statePrefab);
        stateQn.name = "q"+this.count.ToString();
        this.count++;
    }
}
